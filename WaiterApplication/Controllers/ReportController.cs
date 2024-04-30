using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Core.Services;

namespace WaiterApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IReportService reportService;

        public ReportController(IOrderService _orderService,
            IReportService _reportService)
        {
            orderService = _orderService;
            reportService = _reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            var totalAmount = await reportService.TotalAmountEarned();

            var topDishes = await reportService.GetTopDishes();

            var topTables = await reportService.GetTopTables();

            ReportViewModel model = new ReportViewModel()
            {
                TotalAmountEarned = totalAmount,
                TopDishesWithNames = topDishes,
                TopTables = topTables
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DailyReport()
        {
            var totalAmount = await reportService.DailyAmountEarned();

            var topDishes = await reportService.GetDailyTopDishes();

            var topTables = await reportService.GetDailyTopTables();

            ReportViewModel model = new ReportViewModel()
            {
                TotalAmountEarned = totalAmount,
                TopDishesWithNames = topDishes,
                TopTables = topTables
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> WeeklyReport()
        {
            var totalAmount = await reportService.DailyAmountEarned();

            var topDishes = await reportService.GetDailyTopDishes();

            var topTables = await reportService.GetDailyTopTables();

            ReportViewModel model = new ReportViewModel()
            {
                TotalAmountEarned = totalAmount,
                TopDishesWithNames = topDishes,
                TopTables = topTables
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> MonthlyReport()
        {
            var totalAmount = await reportService.DailyAmountEarned();

            var topDishes = await reportService.GetDailyTopDishes();

            var topTables = await reportService.GetDailyTopTables();

            ReportViewModel model = new ReportViewModel()
            {
                TotalAmountEarned = totalAmount,
                TopDishesWithNames = topDishes,
                TopTables = topTables
            };

            return View(model);
        }
    }
}
