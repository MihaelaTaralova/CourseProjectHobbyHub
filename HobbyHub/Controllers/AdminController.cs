using HobbyHub.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HobbyHubSystem.Common;

namespace HobbyHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHobbyService hobbyService;

        public AdminController(IHobbyService hobbyService)
        {
            this.hobbyService = hobbyService;
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
            return RedirectToAction("PendingHobbies");
        }
    }
}
