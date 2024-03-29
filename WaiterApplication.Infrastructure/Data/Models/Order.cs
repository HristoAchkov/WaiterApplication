using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class Order
    {
        [Comment("Order Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Total amount of the ordered dishes")]
        public decimal TotalAmount { get; set; }
        [Required]
        [Range(0,500, ErrorMessage = TableNumbersErrorMessage)]
        [DefaultValue(1)]
        public int TableNumber { get; set; }
        [ForeignKey(nameof(TableNumber))]
        public Table Table { get; set; } = null!;

        public ICollection<OrderDish> OrderedDishes { get; set; } = new List<OrderDish>();
    }
}
