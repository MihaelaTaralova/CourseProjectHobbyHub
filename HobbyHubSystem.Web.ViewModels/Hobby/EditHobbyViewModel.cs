using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class EditHobbyViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

    }
}
