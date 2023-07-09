using HobbyBubSystem.Data.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HobbyHubSystem.Common.EntityValidationConstants.Answer;


namespace HobbyBubSystem.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; } = null!;

        public HobbyUser Author { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Question))] 
        public string QuestionId { get; set; } = null!;

        public Question Question { get; set; } = null!;
        

        public DateTime RepliedOn { get; set; }

    }
}
