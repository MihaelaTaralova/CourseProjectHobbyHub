using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Category;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int categoryId);

        Task AddCategoryAsync(AddCategoryViewModel categoryViewModel);

        Task EditCategory(int categoryId, EditCategoryViewModel model);

        Task DeleteCategory(int categoryId);

        Task<Category> GetCategoryByNameAsync(string name);
    }
}
