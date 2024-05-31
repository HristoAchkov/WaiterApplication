using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class OrdersAndPromotionsServiceModel
    {
        public List<AllDishesOrder> Dishes { get; set; } = null!;
        public List<AllPromotionsViewModel> Promotions { get; set; } = null!;
    }
}
