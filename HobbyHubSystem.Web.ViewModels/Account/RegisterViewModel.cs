using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static HobbyHubSystem.Common.EntityValidationConstants.HobbyUser;

namespace HobbyHubSystem.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(FirstNameMax), MinLength(FirstNameMin)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMax), MinLength(LastNameMin)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        
        [Required]
        public string Gender { get; set; } = null!;
               
        public IFormFile ImageUrl { get; set; } = null!;
    }
}
