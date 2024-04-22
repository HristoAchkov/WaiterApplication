using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class AllDishesOrder
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public string? Ingredients { get; set; }
        public int OrderId { get; set; }
    }
}
