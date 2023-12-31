﻿using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Admin;
using HobbyHubSystem.Web.ViewModels.Article;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync(int hubId);

        Task<Article> GetArticleByIdAsync(int Id);

        Task AddArticleAsync(AddArticleViewModel articleViewModel, Guid AuthorId, bool isApproved = false);

        Task EditArticle(int Id, EditArticleViewModel model);

        Task DeleteArticle(int articleId);

        Task<Article> GetArticleByNameAsync(string title);

        Task<PendingArticlesViewModel> GetPendingArticlesAsync();

        Task ApproveArticleAsync(int Id);

        Task<ArticleViewModel> GetArticleWithAuthorAsync(int articleId);

    }
}
