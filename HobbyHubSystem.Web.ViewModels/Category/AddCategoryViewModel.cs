using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Category;

namespace HobbyHubSystem.Web.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
