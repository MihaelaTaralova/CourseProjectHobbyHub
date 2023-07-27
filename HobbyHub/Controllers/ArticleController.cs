using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Article;
using HobbyHubSystem.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HobbyHub.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService _articleService)
        {
            this.articleService = _articleService;   
        }
        public async Task<IActionResult> All()
        {
            var articles = await articleService.GetAllArticlesAsync();

            var articleViewModel = articles.Select(a => new ArticleIntroViewModel
            {
                Id = a.Id,
                Title = a.Title,
                AuthorName = a.Author.LastName,
                PublishDate = a.PublishDate,
            }).ToList();

            var viewModel = new AllArticleViewModel 
            { 
                Articles = articleViewModel 
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddArticle(int id)
        {
            //if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            //{
            //    return Forbid();
            //}
            AddArticleViewModel model = new()
            {
                HubId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleViewModel articleViewModel)
        {
            //if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            //{
            //    return Forbid();
            //}

            if (ModelState.IsValid)
            {
                await articleService.AddArticleAsync(articleViewModel, new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return RedirectToAction("All");
            }

            return View(articleViewModel);
        }
    }
}
