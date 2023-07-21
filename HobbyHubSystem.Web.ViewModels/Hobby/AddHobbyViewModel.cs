using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class AddHobbyViewModel
    {
        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }

        public bool IsActive { get; set; } 

        public bool IsApproved { get; set; } 
    }
}
