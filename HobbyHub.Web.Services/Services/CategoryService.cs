using HobbyHub.Data;
using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Category;

namespace HobbyHub.Web.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HobbyHubDbContext dbContext;
        private readonly IImageService imageService;

        public CategoryService(HobbyHubDbContext _dbContext,
            IImageService _imageService)
        {
            this.dbContext = _dbContext;
            this.imageService = _imageService;
        }

        public async Task AddCategoryAsync(AddCategoryViewModel categoryViewModel)
        {
            var imageUrl = await imageService.SaveImage(categoryViewModel.ImageUrl);

            var category = new Category()
            {
                Name = categoryViewModel.Name,
                ImageUrl = imageUrl,
                IsActive = true
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditCategory(int categoryId, EditCategoryViewModel model)
        {
           var category = await dbContext.Categories.FindAsync(categoryId);
            if (category == null) 
            {
                throw new ArgumentException("Catgory not found");
            }

            category.Name = model.Name;

            if (model.ImageUrl != null)
            {
                category.ImageUrl = model.ImageUrl;
            }

            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();    
        }

        public async Task DeleteCategory(int Id)
        {
            var category = await dbContext.Categories.FindAsync(Id);
            if (category == null)
            {
                throw new ArgumentException("Category not found.");
            }
            
            category.IsActive = false;
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.Where(c => c.IsActive == true).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
