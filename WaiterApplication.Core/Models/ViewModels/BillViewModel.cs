using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}
