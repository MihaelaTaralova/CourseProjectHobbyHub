using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class HobbyViewModel
    {
        public int HobbyId { get; set; }

        [Required]
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public int HubId { get; set; }

        public bool IsJoinedHub { get; set; }
    }
}
