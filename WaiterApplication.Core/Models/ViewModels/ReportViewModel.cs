using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class ReportViewModel
    {
        public decimal TotalAmountEarned { get; set; }
        public TopDishesModel TopDishesWithNames { get; set; } = null!;
        public TopTablesModel TopTables { get; set; } = null!;
    }
}
