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
    }
}
