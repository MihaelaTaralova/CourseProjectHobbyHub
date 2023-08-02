using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Event;

namespace HobbyHubSystem.Web.ViewModels.Event
{
    public class AddEventViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMax), MinLength(TileMin)]
        public string Title { get; set; } = null!;

        public Guid CreatorId { get; set; }

        [Required]
        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateOfEvent { get; set; }

        [Required]
        [StringLength(LocationMax), MinLength(LocationMin)]
        public string Location { get; set; } = null!;

        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }
    }
}
