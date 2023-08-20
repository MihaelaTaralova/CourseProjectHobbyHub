using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        public async Task<IActionResult> All(int hubId, int articleId) 
        {
            if (hubId <= 0 && articleId <= 0)
            {
                return BadRequest();
            }

            if (articleId > 0)
            {
                var getArticle = await articleService.GetArticleByIdAsync(articleId);
                if (getArticle == null)
                {
                    return BadRequest(); 
                }
                hubId = getArticle.HubId;
            }
            var allArticles = await articleService.GetAllArticlesAsync(hubId);
            var articles = allArticles.Where(a => a.IsActive == true).ToList();

            var articleViewModel = articles.Select(a => new ArticleIntroViewModel
            {
                Id = a.Id,
                Title = a.Title,
                AuthorName = a.Author.LastName,
                PublishDate = a.PublishDate,
                HubId = a.HubId,
                IsApproved = a.IsApproved,
            }).ToList();

            var viewModel = new AllArticleViewModel
            {
                Articles = articleViewModel,
                HubId = hubId
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddArticle(int id)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest();
            }

            AddArticleViewModel model = new()
            {
                HubId = id,
                PublishDate = DateTime.Now,
            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleViewModel articleViewModel)
        {
            var isAppoved = User.IsInRole("Administrator") || User.IsInRole("Moderator");

            if (ModelState.IsValid)
            {
                await articleService.AddArticleAsync(articleViewModel, new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value), isAppoved);
              
                return RedirectToAction("All", "Article", new { hubId = articleViewModel.HubId });
            }

            return View(articleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewArticle(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var articleViewModel = await articleService.GetArticleWithAuthorAsync(id);

            if (articleViewModel == null)
            {
                return NotFound();
            }

            return View("ViewArticle", articleViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var article = await articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            if (article.IsActive == false)
            {
                return NotFound();
            }

            var articleModel = new DeleteArticleViewModel
            {
                Id = article.Id,
                Title = article.Title
            };

            return View(articleModel);
        }

        [HttpPost, ActionName("DeleteArticle")]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> DeleteArticleConfirmed(int id)
        {
            try
            {
                await articleService.DeleteArticle(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> EditArticle(int id)
        {
            if (id <=0)
            {
                return BadRequest();
            }

            var article = await articleService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (article.IsActive == false)
            {
                return NotFound();
            }

            var articleViewModel = new EditArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content
            };

            return View(articleViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(int id, EditArticleViewModel model)
        {
            if (id <=0) 
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await articleService.EditArticle(id, model);
                    return RedirectToAction("ViewArticle", "Article", new { id = model.Id, name = model.Title }, fragment: null);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
                return View(model);
           
        }
    }
}
