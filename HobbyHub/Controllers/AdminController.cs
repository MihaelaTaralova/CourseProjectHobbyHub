using HobbyHub.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HobbyHubSystem.Common;
using HobbyHubSystem.Web.ViewModels.Admin;

namespace HobbyHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHobbyService hobbyService;
        private readonly IArticleService articleService;

        public AdminController(IHobbyService hobbyService, 
            IArticleService _articleService)
        {
            this.hobbyService = hobbyService;
            this.articleService = _articleService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Administrator + "," + RoleConstants.Moderator)]
     public async Task<IActionResult> PendingHobbies()
        {
            var pendingHobbies = await hobbyService.GetPendingHobbiesAsync();
            return View(pendingHobbies);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Administrator + "," + RoleConstants.Moderator)]
        public async Task<IActionResult> ApproveHobby(int hobbyId)
        {
            await hobbyService.ApproveHobbyAsync(hobbyId);
            return RedirectToAction("PendingData");
        }


        [HttpGet]
        [Authorize(Roles = RoleConstants.Administrator + "," + RoleConstants.Moderator)]
        public async Task<IActionResult> PendingArticles()
        {
            var pendingArticles = await articleService.GetPendingArticlesAsync();
            return View(pendingArticles);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Administrator + "," + RoleConstants.Moderator)]
        public async Task<IActionResult> ApproveArticle(int articleId)
        {
            await articleService.ApproveArticleAsync(articleId);
            return RedirectToAction("PendingData");
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Administrator + "," + RoleConstants.Moderator)]
        public async Task<IActionResult> PendingData()
        {
            var pendingHobbies = await hobbyService.GetPendingHobbiesAsync();
            var pendingArticles = await articleService.GetPendingArticlesAsync();

            var viewModel = new PendingDataViewModel
            {
                PendingHobbies = pendingHobbies.PendingHobbies,
                PendingArticles = pendingArticles.PendingArticles
            };

            return View(viewModel);
        }

    }
}
