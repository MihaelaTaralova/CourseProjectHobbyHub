using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Admin;
using HobbyHubSystem.Web.ViewModels.Article;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();

        Task<Article> GetCategoryByIdAsync(int Id);

        Task AddArticleAsync(AddArticleViewModel articleViewModel, Guid AuthorId);

        Task EditArticle(int Id, EditArticleViewModel model);

        Task DeleteArticle(int Id);

        Task<Article> GetArticleByNameAsync(string title);

        Task<PendingArticlesViewModel> GetPendingArticlesAsync();

        Task ApproveArticleAsync(int Id);
    }
}
