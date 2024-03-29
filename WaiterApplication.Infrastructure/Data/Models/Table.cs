using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class Table
    {
        [Comment("Table Identifier")]
        [Key]
        public int Id { get; set; }
        [Comment("Name of the table")]
        [Required]
        public string TableName { get; set; } = string.Empty;
        [Comment("Capacity of the table")]
        [Required]
        public int Capacity { get; set; }
        [Comment("Vacant/Occupied table")]
        [Required]
        public bool Status { get; set; }
    }
}
