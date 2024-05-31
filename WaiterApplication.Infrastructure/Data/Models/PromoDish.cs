using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class PromoDish
    {
        [Comment("Promotion Dish Identifier")]
        public int Id { get; set; }
        [Required]
        public int PromotionId { get; set; }
        [ForeignKey(nameof(PromotionId))]
        public Promotion Promotion { get; set; } = null!;
        [Required]
        public int DishId { get; set; }
        [ForeignKey(nameof(DishId))]
        public Dish Dish { get; set; } = null!;
        [DefaultValue(1)]
        public int Quantity { get; set; }
    }
}
