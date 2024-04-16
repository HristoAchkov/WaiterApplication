using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
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
        public Task CalculateBill()
        {
            return null;
        }
        public Task<IActionResult> InitiatePayment()
        {
            return null;
        }
    }
}
