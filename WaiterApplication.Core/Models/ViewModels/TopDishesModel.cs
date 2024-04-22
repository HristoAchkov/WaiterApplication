using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class TopDishesModel
    {
        public List<string> DishNames { get; set; } = new List<string>();
        public List<int> TimesOrdered { get; set; } = new List<int>();
    }
}
