using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IHubService hubService;

        public MemberController(IMemberService _memberService, IHubService hubService)
        {
            this.memberService = _memberService;
            this.hubService = hubService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(int hubId)
        {
            if (hubId <= 0)
            {
                return BadRequest();
            }

            var members = await memberService.GetHubMembers(hubId);

            var viewModel = new AllMembersViewModel()
            {
                HubId = hubId,
                Members = members.Select(m => new MemberViewModel
                {
                    Id = m.Id.ToString(),
                    Name = $"{m.FirstName} {m.LastName}",
                    UserName = m.UserName,
                    ImageUrl = m.ImageUrl
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("ViewProfile/{username}")]
        public async Task<IActionResult> ViewProfile(string username)
        {
            var user = await memberService.GetHobbyUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ViewProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RegisteredOn = user.RegisteredOn,
                ImageUrl = user.ImageUrl
            };

            return View(viewModel);
        }

    }

}

