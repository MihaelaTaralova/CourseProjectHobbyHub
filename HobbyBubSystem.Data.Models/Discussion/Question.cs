using HobbyBubSystem.Data.Models.Account;
using HobbyBubSystem.Data.Models.Discussion;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HobbyHubSystem.Common.EntityValidationConstants.Question;

namespace HobbyBubSystem.Data.Models
{
    public class Question
    {
        public Question()
        {
            this.AuthorId = Guid.NewGuid();
            this.Answers = new List<Answer>();
        }

        [Comment("unique identifier of the question")]
        [Key]
        public int Id { get; set; }

        [Comment("the body of the question")]
        [Required]
        [MaxLength(ContentMax)]
        public string Content { get; set; } = null!;

        [Comment("the date when the question is asked")]
        public DateTime CreatedOn { get; set; }

        [Comment("the creator of the question")]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual HobbyUser Author { get; set; } = null!;

        [Comment("the discussion to which the question belongs")]
        [Required]
        [ForeignKey(nameof(DiscussionTopic))]
        public string DiscussionTopicId { get; set; } = null!;

        public virtual DiscussionTopic DiscussionTopic { get; set; } = null!;

        [Comment("the count of users who saw the question")]
        public int Views { get; set; }

        [Comment("collection of all the answers of one question")]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
