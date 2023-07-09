using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Event;
using HobbyBubSystem.Data.Models.Account;

namespace HobbyBubSystem.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.CreatorId = Guid.NewGuid();
            this.HobbyUsers = new List<HobbyUser>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public HobbyUser Creator { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(LocationMax)]
        public string Location { get; set; } = null!;

        public virtual ICollection<HobbyUser> HobbyUsers { get; set; }

        public bool IsApproved { get; set; } // Флаг, указващ дали събитието е одобрено от модератора


    }
}
