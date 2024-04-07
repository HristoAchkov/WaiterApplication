using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class Dish
    {
        [Comment("Dish Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Dish name")]
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        [Comment("Brief dish description")]
        [Required]
        [Description]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = string.Empty;
        [Comment("Image URL")]
        [MaxLength(MaxUrlLength)]
        [AllowNull]
        public string Image { get; set; }
        [Comment("Dish price")]
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.00, 1000.00, ErrorMessage = "Price {0} must be between {1} and {2}")]
        public decimal Price { get; set; }
        [Comment("Dish ingredients")]
        public string? Ingredients { get; set; }
    }
}
