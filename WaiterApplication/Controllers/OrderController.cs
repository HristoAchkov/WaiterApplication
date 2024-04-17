using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System.Collections.Immutable;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Enumerations;
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
        public OrderController(IOrderService _orderService,
            ITableService _tableService,
            IMenuService _menuService)
        {
            orderService = _orderService;
            tableService = _tableService;
            menuService = _menuService;
        }
        [HttpGet]
        public async Task<IActionResult> All(List<AllOrdersViewModel> model)
        {
            model = await orderService.AllOrdersAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderTable()
        {
            var model = await tableService.AllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrderNew(int tableId)
        {
            var availableDishes = await menuService.TableAllAsync();
            var model = new OrderFormModel
            {
                TableId = tableId,
                AvailableDishes = availableDishes.Select(dish => new AddDishToOrderViewModel
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Price = dish.Price,
                    Image = dish.Image
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int dishId, int tableId)
        {

            OrderDish dish = new OrderDish()
            {
                DishId = dishId,
                OrderId = 1,
                Quantity = 1
            };

            var orderId = await orderService.CreateOrderAsync(tableId, dish);

            await orderService.AddDishToOrderAsync(orderId, dish.Id);

            return RedirectToAction("OrderConfirmation", new { orderId });

        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await orderService.GetOrderDetailsAsync(orderId);
            return View(order);
        }
    }
}
