using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Event;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace HobbyBubSystem.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.CreatorId = Guid.NewGuid();
            this.HobbyUsers = new List<HobbyUser>();
        }

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("name of the event")]
        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [Comment("user who enters the event in the system")]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [Comment("details of the event")]
        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Comment("date on which the event will happen")]
        [Required]
        public DateTime DateOfEvent { get; set; }

        [Comment("location where the event will happen")]
        [Required]
        [MaxLength(LocationMax)]
        public string Location { get; set; } = null!;

        [Comment("people who will attend the event")]
        public virtual ICollection<HobbyUser> HobbyUsers { get; set; }

        // събитието ще се качва само от модератора


    }
}
