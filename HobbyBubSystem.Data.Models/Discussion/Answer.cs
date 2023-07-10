using HobbyBubSystem.Data.Models.Account;
using HobbyBubSystem.Data.Models.Discussion;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using static HobbyHubSystem.Common.EntityValidationConstants.Answer;


namespace HobbyBubSystem.Data.Models
{
    public class Answer
    {
        public Answer()
        {
            this.AuthorId = Guid.NewGuid();
        }

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("this is the author of the question")]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public HobbyUser Author { get; set; } = null!;

        [Comment("this is the body of the answer")]
        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Comment("this is the question")]
        [ForeignKey(nameof(Question))] 
        public string QuestionId { get; set; } = null!;

        public virtual Question Question { get; set; } = null!;

        [Comment("the date when the answer is given")]
        public DateTime RepliedOn { get; set; }

        [Comment("the topic where the question belongs")]
        [Required]
        [ForeignKey(nameof(DiscussionTopic))]
        public string DiscussionTopicId { get; set; } = null!;

        public virtual DiscussionTopic DiscussionTopic { get; set; } = null!;

    }
}
