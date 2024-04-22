using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Core.Services;

namespace WaiterApplication.Controllers
{
    public class BillingController : BaseController
    {
        private readonly IBillingService billingService;
        private readonly IOrderService orderService;
        public BillingController(IBillingService _billingService,
            IOrderService _orderService)
        {
            billingService = _billingService;
            orderService = _orderService;
        }
        [HttpGet]
        public async Task<IActionResult> CalculateBill(int orderId)
        {
            var billModel = await billingService.CalculateBill(orderId);
            var bill = await billingService.CreateBill(billModel);
            billModel.Id = bill.Id;

            return View(billModel);
        }
        [HttpPost]
        public async Task<IActionResult> CalculateBillConfirm(int billId)
        {
            if (await billingService.BillExistsAsync(billId.ToString()) == false)
            {
                return BadRequest();
            }

            var bill = await billingService.BillDetailsByIdAsync(billId);
            var order = await orderService.GetOrder(bill.OrderId);

            if (order != null)
            {
                await billingService.IsOrderPaid(order.Id);
            }

            await billingService.RemoveBill(billId);

            return RedirectToAction("GetTables","Order");
        }
    }
}
