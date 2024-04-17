using System.ComponentModel.DataAnnotations;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class TableViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(MaxTableNameLength, MinimumLength = TableNameMinLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(MinTableCapacity,MaxTableCapacity)]
        public int Capacity { get; set; }
        [Required]
        public bool Status { get; set; } = false;
    }
}
