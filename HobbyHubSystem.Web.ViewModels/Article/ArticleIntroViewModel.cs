using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Web.ViewModels.Article
{
    public class ArticleIntroViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public int HobbyId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
