
namespace HobbyHubSystem.Web.ViewModels.Article
{
    public class AllArticleViewModel
    {
        public AllArticleViewModel()
        {
            this.Articles = new List<ArticleIntroViewModel>();
        }
        public int HubId { get; set; }

        public List<ArticleIntroViewModel> Articles { get; set; }

    }
}
