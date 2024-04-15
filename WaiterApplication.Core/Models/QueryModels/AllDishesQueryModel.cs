using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Enumerations;

namespace WaiterApplication.Core.Models.QueryModels
{
    public class AllDishesQueryModel
    {
        public int DishesPerPage { get; } = 6;
        [Display(Name = "SearchTerm by text")]
        public string SearchTerm { get; set; } = string.Empty;

        public DishSorting Sorting { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalDishesCount { get; set; }
        public IEnumerable<AllDishesServiceModel> Dishes { get; set; } = new List<AllDishesServiceModel>();
    }
}
