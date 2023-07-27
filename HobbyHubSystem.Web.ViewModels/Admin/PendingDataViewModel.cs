using HobbyHubSystem.Web.ViewModels.Article;
using HobbyHubSystem.Web.ViewModels.Hobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Web.ViewModels.Admin
{
    public class PendingDataViewModel
    {
        public List<HobbyViewModel> PendingHobbies { get; set; }
        public List<AddArticleViewModel> PendingArticles { get; set; }
    }
}
