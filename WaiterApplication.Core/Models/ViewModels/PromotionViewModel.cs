using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class PromotionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<AllDishesServiceModel> Dishes { get; set; } = new List<AllDishesServiceModel>();
    }
}
