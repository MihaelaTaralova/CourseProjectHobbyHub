using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Article;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Web.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly HobbyHubDbContext dbContext;

        public ArticleService(HobbyHubDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddArticleAsync(AddArticleViewModel articleViewModel, Guid AuthorId)
        {
            var article = new Article()
            {
                Title = articleViewModel.Title,
                Content = articleViewModel.Content,
                AuthorId = AuthorId,
                PublishDate = articleViewModel.PublishDate,
                HubId = articleViewModel.HubId,
                IsApproved = false
            };

           await dbContext.AddAsync(article);
           await dbContext.SaveChangesAsync();
        }

        public async Task ApproveArticleAsync(int Id)
        {
            var article = await dbContext.Articles.FindAsync(Id);

            if (article == null) 
            {
                throw new ArgumentException("Article not found");
            }

            article.IsApproved = true;
            dbContext.Update(article);
            await dbContext.SaveChangesAsync();
        }

        public Task DeleteArticle(int Id)
        {
            throw new NotImplementedException();
        }

        public Task EditArticle(int Id, EditArticleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
           return await dbContext.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleByNameAsync(string title)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(a => a.Title == title);
        }

        public async Task<Article> GetCategoryByIdAsync(int Id)
        {
            return await dbContext.Articles.FindAsync(Id);
        }

        public async Task<PendingArticlesViewModel> GetPendingArticlesAsync()
        {
            var pendingArticles = await dbContext.Articles.Where(a => !a.IsApproved).ToListAsync();
            var viewModel = new PendingArticlesViewModel()
            {
                PendingArticles = pendingArticles.Select(a => new AddArticleViewModel
                { 
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    PublishDate = a.PublishDate,
                    AuthorId = a.AuthorId,
                    HubId = a.HubId,
                    IsApproved = a.IsApproved
                }).ToList(),
            };

            return viewModel;
        }
    }
}
