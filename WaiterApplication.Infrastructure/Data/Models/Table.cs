using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class Table
    {
        [Comment("Table Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Name of the table")]
        [Required]
        [MaxLength(MaxTableNameLength)]
        public string TableName { get; set; } = string.Empty;
        [Comment("Capacity of the table")]
        [Required]
        public int Capacity { get; set; }
        [Comment("Vacant/Occupied table")]
        [Required]
        public bool Status { get; set; }
    }
}
