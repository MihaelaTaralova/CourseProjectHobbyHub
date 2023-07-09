using HobbyBubSystem.Data.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HobbyHubSystem.Common.EntityValidationConstants.Article;

namespace HobbyBubSystem.Data.Models
{
    public class Article
    {
        public Article()
        {
            this.AuthorId = Guid.NewGuid();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMax)]
        public string Content { get; set; } = null!;

        public DateTime PublishDate { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public HobbyUser Author { get; set; } = null!;

        public bool IsApproved { get; set; } // Флаг, указващ дали статията е одобрено от модератора

    }
}
