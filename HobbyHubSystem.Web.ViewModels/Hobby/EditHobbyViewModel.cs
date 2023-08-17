using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyHubSystem.Web.ViewModels.Hobby
{
    public class EditHobbyViewModel
    {
        public int Id { get; set; }
               
        [StringLength(NameMax), MinLength(NameMin)]
        public string Name { get; set; } = null!;

        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; } = null!;
                
        public IFormFile? ImageFile { get; set; }
               
        public string CurrentImageUrl { get; set; }

    }
}
