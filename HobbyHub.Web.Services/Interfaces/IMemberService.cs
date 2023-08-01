using HobbyBubSystem.Data.Models.Account;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<HobbyUser>> GetHubMembers(int hubId);

        Task<HobbyUser> GetHobbyUserByUsernameAsync(string username);

    }
}
