using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HobbyBubSystem.Data.Models.Account;
using static HobbyHubSystem.Common.EntityValidationConstants.Hub;
using HobbyBubSystem.Data.Models.Discussion;
using Microsoft.EntityFrameworkCore;

namespace HobbyBubSystem.Data.Models
{
    public class Hub
    {
        public Hub()
        {
            this.Articles = new List<Article>();
            this.Events = new List<Event>();
            this.DiscussionTopics = new List<DiscussionTopic>();
            this.Members = new HashSet<HobbyUserHub>();
        }
        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("name of the hub")]
        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Comment("short description of the hub")]
        [Required]
        [MaxLength(AboutMax)]
        public string About { get; set; } = null!;

        [Comment("the creator of the group")]
        [Required]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [Comment("hobby to which this group belongs")]
        [ForeignKey(nameof(Hobby))]
        public int HobbyId { get; set; }

        public virtual Hobby Hobby { get; set; } = null!;

        [Comment("collection with articles")]
        public virtual ICollection<Article> Articles { get; set; }

        [Comment("events which are bound with the hobby")]
        public virtual ICollection<Event> Events { get; set; }

        [Comment("discussion topic in the forum")]
        public virtual ICollection<DiscussionTopic> DiscussionTopics { get; set; }

        [Comment("memebers of the group")]
        public virtual ICollection<HobbyUserHub> Members { get; set; }
    }
}
