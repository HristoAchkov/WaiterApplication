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
    public class Bill
    {
        [Comment("Bill Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Is payment complete?")]
        [Required]
        public bool PaymentCompleted { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
    }
}
