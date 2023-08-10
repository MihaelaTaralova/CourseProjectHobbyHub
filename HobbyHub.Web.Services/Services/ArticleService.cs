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

        public async Task AddArticleAsync(AddArticleViewModel articleViewModel, Guid AuthorId, bool isApproved = false)
        {
            var article = new Article()
            {
                Title = articleViewModel.Title,
                Content = articleViewModel.Content,
                AuthorId = AuthorId,
                PublishDate = articleViewModel.PublishDate,
                HubId = articleViewModel.HubId,
                IsApproved = isApproved
            };

            await dbContext.Articles.AddAsync(article);
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

        public async Task DeleteArticle(int articleId)
        {
            var article = await dbContext.Articles.FindAsync(articleId);

            if (article == null)
            {
                throw new ArgumentException("Article not found");
            }

            article.IsActive = false;
            dbContext.Articles.Update(article);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditArticle(int Id, EditArticleViewModel model)
        {
            var article = await dbContext.Articles.FindAsync(Id);

            if(article == null) 
            {
                throw new ArgumentException("Article not found");
                    
            }

            article.Title = model.Title;
            article.Content = model.Content;

            dbContext.Articles.Update(article);
            await dbContext.SaveChangesAsync();
        }
               
        public async Task<Article> GetArticleByNameAsync(string title)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(a => a.Title == title);
        }

        public async Task<Article> GetArticleByIdAsync(int Id)
        {
            return await dbContext.Articles.FindAsync(Id);
        }

        public async Task<List<Article>> GetAllArticlesAsync(int hubId)
        {
            return await dbContext.Articles.Include(a => a.Author).Where(a => a.HubId == hubId && a.IsApproved).ToListAsync();
        }

        public async Task<ArticleViewModel> GetArticleWithAuthorAsync(int articleId)
        {
            var article = await dbContext.Articles
                .Include(a => a.Author)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null || !article.IsApproved)
            {
                throw new ArgumentException("Article not found");
            }

            var viewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                AuthorName = article.Author.UserName,
                HubId = article.HubId
            };

            return viewModel;
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
                    HubId = a.HubId
                }).ToList(),
            };

            return viewModel;
        }

    }
}
