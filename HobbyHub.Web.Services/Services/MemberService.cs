using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Member;
using HobbyHubSystem.Web.ViewModels.Member.ServiceModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Web.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly HobbyHubDbContext dbContext;

        public MemberService(HobbyHubDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<List<HobbyUser>> GetHubMembers(int hubId)
        {
            var members = await dbContext.HobbyUserHubs
                .Where(h => h.HubId == hubId)
                .Select(h => h.HobbyUser)
                .ToListAsync();

            return members;
        }

        public async Task<HobbyUser> GetHobbyUserByUsernameAsync(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

    }
}
