using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class Dish
    {
        [Comment("Dish Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Dish name")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Comment("Brief dish description")]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Comment("Image URL")]
        [Required]
        public string Image { get; set; } = string.Empty;
        [Comment("Dish price")]
        [Required]
        public decimal Price { get; set; }
        [Comment("Dish ingredients")]
        public string Ingredients { get; set; }
    }
}
