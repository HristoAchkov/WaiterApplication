using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class OrderFormModel
    {
        public int TableId { get; set; }
        public ICollection<AddDishToOrderViewModel> AvailableDishes { get; set; } = new List<AddDishToOrderViewModel>();
    }
}
