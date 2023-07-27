using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Web.ViewModels.Article
{
    public class AllArticleViewModel
    {
        public AllArticleViewModel()
        {
            this.Articles = new List<ArticleIntroViewModel>();
        }
        public List<ArticleIntroViewModel> Articles { get; set; }
    }
}
