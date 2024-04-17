using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;

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
        public async Task<IActionResult> CreateOrder(int tableId)
        {
            var table = await tableService.TableDetailsByIdAsync(tableId);

            if (await tableService.TableExistsAsync(table.Id.ToString()) == false)
            {
                return BadRequest();
            }

            if (table.Status == true)
            {
                return BadRequest();
            }

            table.Status = true;
            var allDishesAvailable = await menuService.TableAllAsync();

            List<AddDishToOrderViewModel> model = allDishesAvailable
                .Select(x => new AddDishToOrderViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Price = x.Price,
                }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(List<AddDishToOrderViewModel> model,[FromQuery] int tableId)
        {
            if (await tableService.TableExistsAsync(tableId.ToString()) == false)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = new OrderFormModel();
            foreach (var item in model)
            {
                // Perform any necessary operations on each item
            }

            // Redirect to a success page or return a view
            return RedirectToAction(nameof(All)); // Replace "ActionName" with the appropriate action
        }
        //[HttpPost]
        //public async Task<IActionResult> OrderConfirmation(List<AddDishToOrderViewModel> model)
        //{
        //    await orderService.CreateAsync(model);
        //    return RedirectToAction(nameof(All));
        //}
    }
}
