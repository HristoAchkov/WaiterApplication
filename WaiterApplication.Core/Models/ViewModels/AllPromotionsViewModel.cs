using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class AllPromotionsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<PromoDish> Dishes { get; set; } = null!;
    }
}
