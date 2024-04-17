using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class InventoryItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? UnitOfMeasurement { get; set; }
    }
}
