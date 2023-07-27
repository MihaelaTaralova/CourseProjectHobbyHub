using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
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

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("title of the article")]
        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [Comment("content of the article")]
        [Required]
        [MaxLength(ContentMax)]
        public string Content { get; set; } = null!;

        [Comment("the date on which the article is published")]
        public DateTime PublishDate { get; set; }

        [Comment("author of the article")]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual HobbyUser Author { get; set; } = null!;

        [Comment("hobby to which the article belongs")]
        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }

        public virtual Hub Hub { get; set; } = null!;

        [Comment("before release the article should be approved by admin or moderator")]
        public bool IsApproved { get; set; } 
    }
}
