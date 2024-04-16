using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class InventoryItem
    {
        [Comment("Inventory Item Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Name of the item")]
        [Required]
        [MaxLength(InventoryItemMaxLength)]
        [MinLength(InventoryItemMinLength)]
        public string Name { get; set; } = string.Empty;
        [Comment("Quantity of the item")]
        [Required]
        public int Quantity { get; set; }
        [Comment("Item unit of measurement: kg, l, mg, etc...")]
        public string? UnitOfMeasurement { get; set; }
    }
}
