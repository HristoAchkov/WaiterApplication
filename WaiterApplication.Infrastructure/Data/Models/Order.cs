using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int TableNumber { get; set; }
        [ForeignKey(nameof(TableNumber))]
        public Table Table { get; set; } = null!;

        public ICollection<OrderDish> OrderedDishes { get; set; } = new List<OrderDish>();
    }
}
