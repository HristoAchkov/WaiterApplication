using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Core.Services;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ITableService tableService;
        private readonly IMenuService menuService;
        private readonly IPromotionService promotionService;
        public OrderController(IOrderService _orderService,
            ITableService _tableService,
            IMenuService _menuService,
            IPromotionService _promotionService)
        {
            orderService = _orderService;
            tableService = _tableService;
            menuService = _menuService;
            promotionService = _promotionService;
        }
        [HttpGet]
        public async Task<IActionResult> All(List<AllOrdersViewModel> model)
        {
            model = await orderService.AllOrdersAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var model = await tableService.AllAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder([FromQuery]int tableId)
        {
            if (await tableService.TableExistsAsync(tableId.ToString()) == false)
            {
                return BadRequest();
            }

            var table = await tableService.TableDetailsByIdAsync(tableId);

            if (table.Status == true)
            {
                return RedirectToAction(nameof(All));
            }
            else
            {
                table.Status = true;
            }

            var tableModel = await orderService.GetTableAsync(tableId);
            tableModel.Status = table.Status;

            OrderViewModel order = new OrderViewModel()
            {
                TableId = tableId,
                OrderDishes = new List<OrderDish>()
            };

            var model = new Order()
            {
                TableNumber = tableId,
                OrderedDishes = order.OrderDishes,
                CreatedOn = DateTime.Now
            };
            await orderService.CreateOrderAsync(model);
            order.OrderId = model.Id;

            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> AllDishesMenu(int orderId)
        {
            var dishes = await orderService.AllDishes(orderId);
            var promotions = await promotionService.AllPromotions();

            var combo = new OrdersAndPromotionsServiceModel()
            {
                Dishes = dishes,
                Promotions = promotions
            };

            return View(combo);
        }
        [HttpPost]
        public async Task<IActionResult> AddDish(int dishId,int orderId, string? comment)
        {
            var orderDish = new OrderDish()
            {
                DishId = dishId,
                Dish = await orderService.GetDishDetailsByIdAsync(dishId),
                OrderId = orderId,
                Quantity = 1,
                Comment = comment
            };

            await orderService.AddOrderDishToOrder(orderDish, orderId);

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int orderId)
        {
            var model = await orderService.GetOrderDetailsByIdAsync(orderId);

            var dishes = new List<Dish>();

            foreach (var dishId in model.OrderDishes.Select(x => x.DishId))
            {
                var dishDetails = await orderService.GetDishDetailsByIdAsync(dishId);
                dishes.Add(dishDetails);
            }

            foreach (var item in model.OrderDishes)
            {
                var dish = dishes.FirstOrDefault(d => d.Id == item.DishId);
                item.Dish = dish;
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddAnotherDish(int orderId)
        {
            var dishes = await orderService.AllDishes(orderId);

            return View(dishes);
        }
        [HttpPost]
        public async Task<IActionResult> AddAnotherDish(int dishId, int orderId, string? comment)
        {
            var orderDish = new OrderDish()
            {
                DishId = dishId,
                Dish = await orderService.GetDishDetailsByIdAsync(dishId),
                OrderId = orderId,
                Quantity = 1,
                Comment = comment
            };

            await orderService.AddOrderDishToOrder(orderDish, orderId);

            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveOrder(int tableId,int orderId)
        {
            var order = await orderService.GetOrderDetailsByIdAsync(orderId);
            List<int> dishIds = order.OrderDishes.Select(x => x.Id).ToList();

            await orderService.RemoveOrderDishAndOrder(tableId,orderId, dishIds);
            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveDishFromOrder(int orderId, int dishId)
        {
            var order = await orderService.GetOrderDetailsByIdAsync(orderId);
            await orderService.RemoveOrderDishFromOrder(dishId);

            return RedirectToAction(nameof(Details), new {orderId = orderId});
        }
    }
}
