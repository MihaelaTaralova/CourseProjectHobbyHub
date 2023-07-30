using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class HobbiesInCategoryViewModel
    {
        public int HobbyId { get; set; }

        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public bool IsApproved { get; set; }
    }
}
