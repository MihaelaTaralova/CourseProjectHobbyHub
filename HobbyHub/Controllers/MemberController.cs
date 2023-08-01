using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService _memberService)
        {
            this.memberService = _memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(int hubId)
        {
            var members = await memberService.GetHubMembers(hubId);

            var viewModel = new AllMembersViewModel()
            {
                HubId = hubId,
                Members = members.Select(m => new MemberViewModel
                {
                    Id = m.Id.ToString(),
                    Name = $"{m.FirstName} {m.LastName}",
                    ImageUrl = m.ImageUrl
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
