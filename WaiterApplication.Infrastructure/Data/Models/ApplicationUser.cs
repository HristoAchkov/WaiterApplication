using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        [MaxLength(UserFirstNameMaxLength)]
        [MinLength(UserFirstNameMinLength)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [PersonalData]
        [MaxLength(UserLastNameMaxLength)]
        [MinLength(UserLastNameMinLength)]
        public string LastName { get; set; } = string.Empty;
    }
}
