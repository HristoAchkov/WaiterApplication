using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class TopTablesModel
    {
        public List<string> TableNumber { get; set; } = new List<string>();
        public List<int> OrderCount { get; set; } = new List<int>();
    }
}
