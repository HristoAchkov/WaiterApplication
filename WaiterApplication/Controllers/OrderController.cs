using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Core.Services;
using WaiterApplication.Extensions;

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
        public async Task<IActionResult> All(List<AllOrdersViewModel> model)
        {
            model = await orderService.AllOrdersAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int tableId)
        {
            var table = await tableService.TableDetailsByIdAsync(tableId);

            if (await orderService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }

            if (await tableService.TableExistsAsync(table.Id.ToString()) == false)
            {
                return BadRequest();
            }

            if (table.Status == true)
            {
                return BadRequest();
            }

            var model = new OrderFormModel();
            table.Status = true;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDishesToOrder(OrderFormModel model, int dishId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await menuService.DishExistsAsync(dishId.ToString()) == false)
            {
                return BadRequest();
            }



            return null;
        }
    }
}
