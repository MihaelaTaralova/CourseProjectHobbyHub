
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HobbyBubSystem.Data.Models.Account;
using static HobbyHubSystem.Common.EntityValidationConstants.DiscussionTopic;

namespace HobbyBubSystem.Data.Models.Discussion
{
    public class DiscussionTopic
    {
        public DiscussionTopic()
        {
            this.Questions = new List<Question>();
            this.Answers = new List<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(Answer))]
        public Guid AuthorId { get; set; }

        public HobbyUser Author { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
