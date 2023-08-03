using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Hub;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HobbyHub.Controllers
{
    public class HubController : Controller
    {
        private readonly IHubService hubService;

        public HubController(IHubService _hubService)
        {
            this.hubService = _hubService;
        }


        [HttpPost]
        public async Task<IActionResult> JoinHub(int Id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest();
            }

            var isJoined = await hubService.IsUserJoinedHub(Id, userId);

            if (isJoined)
            {
                return Conflict(new { hubId = Id });
            }

            try
            {
                await hubService.JoinHubAsync(Id, userId);
                return Ok(new { hubId = Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("OpenCategory", "Category");
            }

        }

        [HttpGet]
        public async Task<IActionResult> WelcomeHub(int id)
        {
            var hub = await hubService.GetHubByIdAsync(id);

            if (hub == null)
            {
                return NotFound();
            }

            var hubViewModel = new WelcomeHubViewModel
            {
                Id = hub.Id,
                Name = hub.Name,
                About = hub.About
            };

            return View("WelcomeHub", hubViewModel);
        }

    }
}
