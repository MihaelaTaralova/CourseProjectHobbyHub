using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Web.Services.Services
{
    public class HubService : IHubService
    {
        private readonly HobbyHubDbContext dbContext;

        public HubService(HobbyHubDbContext _dbContext)
        {
            this.dbContext = _dbContext;    
        }

        public async Task JoinHubAsync(int Id, Guid userId)
        {
            var hub = await dbContext.Hubs.FindAsync(Id);

            if (hub == null)
            {
                throw new ArgumentException("Hub not found.");
            }

            var existingHubMembership = await dbContext.HobbyUserHubs.FirstOrDefaultAsync(h => h.HubId == Id && h.HobbyUserId == userId);

            if (existingHubMembership != null)
            {
                throw new ArgumentException("You are already a member of this hub.");
            }

            var hubMembership = new HobbyUserHub
            {
                HubId = Id,
                HobbyUserId = userId
            };

            dbContext.HobbyUserHubs.Add(hubMembership);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsUserJoinedHub(int hubId, Guid userId)
        {
            var existingHubMembership = await dbContext.HobbyUserHubs
                .FirstOrDefaultAsync(m => m.HubId == hubId && m.HobbyUserId == userId);

            return existingHubMembership != null;
        }

        public async Task<Hub> GetHubByIdAsync(int hubId)
        {
            return await dbContext.Hubs.FindAsync(hubId);
        }

        public Task<List<HobbyUser>> GetAllMembersAsync(int hubId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
