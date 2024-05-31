using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public decimal TotalAmount 
        { 
            get => OrderDishes.Sum(x => x.Dish.Price * x.Quantity);
        }
        public List<OrderDish> OrderDishes { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
