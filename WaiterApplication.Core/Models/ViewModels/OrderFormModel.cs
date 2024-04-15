using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModel
{
    public class OrderFormModel
    {
        [Display(Name = "Dish")]
        public ICollection<Dish> OrderedDishes { get; set; } = new List<Dish>();
        [Display(Name = "Optional Comment")]
        public string? Comment { get; set; }
    }
}
