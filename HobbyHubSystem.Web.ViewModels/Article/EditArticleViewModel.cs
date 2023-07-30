using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Article;

namespace HobbyHubSystem.Web.ViewModels.Article
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMax), MinLength(TileMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMax), MinLength(ContentMin)]
        public string Content { get; set; } = null!;
    }
}
