using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models;
using WaiterApplication.Extensions;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            if (await orderService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }
            var model = new CreateOrderModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderModel model)
        {
            if (await orderService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }

            await orderService.CreateAsync(model.OrderedDishes);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
