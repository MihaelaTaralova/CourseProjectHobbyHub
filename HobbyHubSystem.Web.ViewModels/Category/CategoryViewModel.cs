using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Category;


namespace HobbyHubSystem.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

    }
}
