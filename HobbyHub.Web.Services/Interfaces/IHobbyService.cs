﻿using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Admin;
using HobbyHubSystem.Web.ViewModels.Category;
using HobbyHubSystem.Web.ViewModels.Hobby;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IHobbyService
    {
        Task<List<Hobby>> GetAllHobbiesAsync();

        Task<List<Hobby>> GetHobbiesByCategoryId(int categoryId);

        Task<Hobby> GetHobbyByIdAsync(int categoryId);

        Task AddHobbyAsync(AddHobbyViewModel hobbyViewModel, Guid userId);

        Task EditHobbyAsync(int categoryId, EditHobbyViewModel model);

        Task DeleteHobbyAsync(int categoryId);

        Task<PendingHobbiesViewModel> GetPendingHobbiesAsync();

        Task ApproveHobbyAsync(int hobbyId);

    }
}
