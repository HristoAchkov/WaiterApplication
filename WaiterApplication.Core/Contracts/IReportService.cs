using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.Core.Contracts
{
    public interface IReportService
    {
        Task<decimal> TotalAmountEarned();
        Task<TopDishesModel> GetTopDishes();
        Task<TopTablesModel> GetTopTables();
        Task<decimal> DailyAmountEarned();
        Task<TopDishesModel> GetDailyTopDishes();
        Task<TopTablesModel> GetDailyTopTables();
        Task<decimal> WeeklyAmountEarned();
        Task<TopDishesModel> GetWeeklyTopDishes();
        Task<TopTablesModel> GetWeeklyTopTables();
        Task<decimal> MonthlyAmountEarned();
        Task<TopDishesModel> GetMonthlyTopDishes();
        Task<TopTablesModel> GetMonthlyTopTables();
    }
}
