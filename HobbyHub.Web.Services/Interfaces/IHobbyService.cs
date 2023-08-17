using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Admin;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Http;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IHobbyService
    {
        Task<List<Hobby>> GetAllHobbiesAsync();

        Task<List<Hobby>> GetHobbiesByCategoryId(int categoryId);

        Task<Hobby> GetHobbyByIdAsync(int categoryId);

        Task AddHobbyAsync(AddHobbyViewModel hobbyViewModel, Guid userId, bool isApproved = false);

        Task EditHobbyAsync(int categoryId, EditHobbyViewModel model, IFormFile imageFile);

        Task DeleteHobbyAsync(int categoryId);

        Task<PendingHobbiesViewModel> GetPendingHobbiesAsync();

        Task ApproveHobbyAsync(int hobbyId);

        Task<string> GetHobbyImageUrlById(int hobbyId);

    }
}
