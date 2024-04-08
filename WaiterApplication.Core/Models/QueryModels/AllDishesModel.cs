using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.QueryModels
{
    public class AllDishesModel
    {
        public int TotalDishesCount { get; set; }
        public IEnumerable<AllDishesServiceModel> Dishes { get; set; } = new HashSet<AllDishesServiceModel>();
    }
}
