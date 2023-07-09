using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HobbyBubSystem.Data.Models.Account;
using static HobbyHubSystem.Common.EntityValidationConstants.Hub;
using HobbyBubSystem.Data.Models.Discussion;

namespace HobbyBubSystem.Data.Models
{
    public class Hub
    {
        public Hub()
        {
         this.Articles = new List<Article>();
            this.Events = new List<Event>();
            this.Members = new List<HobbyUser>();
            this.DiscussionTopics = new List<DiscussionTopic>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(AboutMax)]
        public string About { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [ForeignKey(nameof(Hobby))]
        public int HobbyId { get; set; }

        public virtual Hobby Hobby { get; set; } = null!;

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<HobbyUser> Members { get; set; }

        public virtual ICollection<DiscussionTopic> DiscussionTopics { get; set; }
    }
}
