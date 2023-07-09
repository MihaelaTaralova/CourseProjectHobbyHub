using HobbyBubSystem.Data.Models.Account;
using HobbyBubSystem.Data.Models.Discussion;
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

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMax)]
        public string Content { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(Answer))]
        public Guid AuthorId { get; set; }

        public HobbyUser Author { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(DiscussionTopic))]
        public string DiscussionTopicId { get; set; } = null!;

        public DiscussionTopic DiscussionTopic { get; set; } = null!;
        
        public int Views { get; set; }

        ICollection<Answer> Answers { get; set; }
    }
}
