using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class AllOrdersViewModel
    {
        [Display(Name = "Table")]
        public string TableNumber { get; set; } = string.Empty;
        [Display(Name = "Amount")]
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
    }
}
