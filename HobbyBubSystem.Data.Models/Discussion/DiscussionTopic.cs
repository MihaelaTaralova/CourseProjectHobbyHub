
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HobbyBubSystem.Data.Models.Account;
using static HobbyHubSystem.Common.EntityValidationConstants.DiscussionTopic;
using Microsoft.EntityFrameworkCore;

namespace HobbyBubSystem.Data.Models.Discussion
{
    public class DiscussionTopic
    {
        public DiscussionTopic()
        {
            this.Questions = new List<Question>();
            this.Answers = new List<Answer>();
        }

        [Comment("unique identifier of the discussion topic")]
        [Key]
        public int Id { get; set; }

        [Comment("title of the discussion topic")]
        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [Comment("the date when the topic is created")]
        public DateTime CreateDate { get; set; }

        [Comment("author of the topic")]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual HobbyUser Author { get; set; } = null!;

        [Comment("collection with all the questions concerning one topic")]
        public virtual ICollection<Question> Questions { get; set; }

        [Comment("collection with all the answers concerning one question in the topic")]
        public virtual ICollection<Answer> Answers { get; set; } // това трябва ли да го има
    }
}
