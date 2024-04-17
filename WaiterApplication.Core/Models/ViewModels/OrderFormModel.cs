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
        [Display(Name = "Dish")]
        public ICollection<AddDishToOrderViewModel> OrderedDishes { get; set; } = new List<AddDishToOrderViewModel>();
        [Display(Name = "Optional Comment")]
        public ICollection<string>? Comments { get; set; } = new List<string>();
    }
}
