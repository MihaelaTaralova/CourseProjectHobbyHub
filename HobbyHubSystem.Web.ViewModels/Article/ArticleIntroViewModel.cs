
namespace HobbyHubSystem.Web.ViewModels.Article
{
    public class ArticleIntroViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public int HubId { get; set; }
        public DateTime PublishDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
