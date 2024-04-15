using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Core.Models.QueryModels
{
    public class AllDishesServiceModel : IDishModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Ingredients { get; set; }
    }
}
