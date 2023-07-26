using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IHubService
    {
        Task JoinHubAsync(int hubId, Guid userId);

        Task<bool> IsUserJoinedHub(int hubId, Guid userId);

        Task<Hub> GetHubByIdAsync(int hubId);

        Task<List<HobbyUser>> GetAllMembersAsync(int hubId, Guid userId);
    }
}
