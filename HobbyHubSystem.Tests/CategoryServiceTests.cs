using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static HobbyHubSystem.Tests.DatabaseSeeder;

namespace HobbyHubSystem.Tests
{
    public class CategoryServiceTests
    {

        private DbContextOptions<HobbyHubDbContext> options;
        private HobbyHubDbContext dbContext;
        private string tempImageDirectory;
        private ICategoryService categoryService;

        [SetUp]
        public void Setup()
        {
            tempImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TempImages");
            Directory.CreateDirectory(tempImageDirectory);

            this.options = new DbContextOptionsBuilder<HobbyHubDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            dbContext = new HobbyHubDbContext(options);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            var mockImageService = new MockImageService();
            this.categoryService = new CategoryService(dbContext, mockImageService);

        }

        [TearDown]
        public void CleanUp()
        {
            dbContext.Dispose();
            Directory.Delete(tempImageDirectory, true);
        }

        [Test]
        public async Task AddCategoryAsync_ShouldAddCategoryToDatabase()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "This is a mock image content");

            using var stream = new FileStream(tempFile, FileMode.Open);

            var formFile = new FormFile(
                stream,
                0,
                stream.Length,
                null,
                Path.GetFileName(tempFile)
            );

            // Arrange
            var categoryViewModel = new AddCategoryViewModel
            {
                Name = "Test Category",
                ImageUrl = formFile,
            };

            // Act
            await categoryService.AddCategoryAsync(categoryViewModel);

            // Assert
            var addedCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryViewModel.Name);
            Assert.NotNull(addedCategory);
            Assert.AreEqual(categoryViewModel.Name, addedCategory.Name);
            
        }

        [Test]
        public async Task EditCategory_WithValidData_ShouldUpdateCategory()
        {
            // Arrange
            var categoryToEdit = await dbContext.Categories.FirstAsync();
            var editCategoryViewModel = new EditCategoryViewModel
            {
                Name = "Updated Category",
                ImageUrl = "updated-image.jpg"
            };

            // Act
            await categoryService.EditCategory(categoryToEdit.Id, editCategoryViewModel);

            // Assert
            var updatedCategory = await dbContext.Categories.FindAsync(categoryToEdit.Id);
            Assert.NotNull(updatedCategory);
            Assert.AreEqual(editCategoryViewModel.Name, updatedCategory.Name);
            Assert.AreEqual(editCategoryViewModel.ImageUrl, updatedCategory.ImageUrl);
        }

        [Test]
        public async Task EditCategory_WithInvalidCategoryId_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistentCategoryId = -1;
            var editCategoryViewModel = new EditCategoryViewModel
            {
                Name = "Updated Category",
                ImageUrl = "updated-image.jpg"
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await categoryService.EditCategory(nonExistentCategoryId, editCategoryViewModel);
            });
        }

        [Test]
        public async Task DeleteCategory_WithValidCategoryId_ShouldSetCategoryAsInactive()
        {
            // Arrange
            var categoryToDelete = await dbContext.Categories.FirstAsync();

            // Act
            await categoryService.DeleteCategory(categoryToDelete.Id);

            // Assert
            var deletedCategory = await dbContext.Categories.FindAsync(categoryToDelete.Id);
            Assert.NotNull(deletedCategory);
            Assert.IsFalse(deletedCategory.IsActive);
        }

        [Test]
        public async Task DeleteCategory_WithInvalidCategoryId_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistentCategoryId = -1;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await categoryService.DeleteCategory(nonExistentCategoryId);
            });
        }

        [Test]
        public async Task GetAllCategoriesAsync_ShouldReturnAllActiveCategories()
        {
            // Arrange
            var category1 = new Category
            {
                Name = "Category 1",
                IsActive = true,
                ImageUrl = "category1-image.jpg"
            };
            var category2 = new Category
            {
                Name = "Category 2",
                IsActive = true,
                ImageUrl = "category2-image.jpg"
            };
            var category3 = new Category
            {
                Name = "Category 3",
                IsActive = false,
                ImageUrl = "category3-image.jpg"
            };
            dbContext.Categories.AddRange(category1, category2, category3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(6, result.Count);
            Assert.IsTrue(result.Any(c => c.Name == "Category 1"));
            Assert.IsTrue(result.Any(c => c.Name == "Category 2"));
            Assert.IsFalse(result.Any(c => c.Name == "Category 3"));
        }

        [Test]
        public async Task GetCategoryByIdAsync_ShouldReturnCorrectCategory()
        {
            // Arrange
            var category = new Category
            {
                Id = 1005,
                Name = "Test Category",
                IsActive = true,
                ImageUrl = "test-image.jpg"
            };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await categoryService.GetCategoryByIdAsync(1005);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Test Category", result.Name);
            Assert.AreEqual("test-image.jpg", result.ImageUrl);
        }

        [Test]
        public async Task GetCategoryByNameAsync_ShouldReturnCorrectCategory()
        {
            // Arrange
            var category1 = new Category
            {
                Id = 101,
                Name = "Test Category 1",
                IsActive = true,
                ImageUrl = "test-image1.jpg"
            };

            var category2 = new Category
            {
                Id = 202,
                Name = "Test Category 2",
                IsActive = true,
                ImageUrl = "test-image2.jpg"
            };

            dbContext.Categories.AddRange(category1, category2);
            await dbContext.SaveChangesAsync();

            // Act
            var resultCategory1 = await categoryService.GetCategoryByNameAsync("Test Category 1");
            var resultCategory2 = await categoryService.GetCategoryByNameAsync("Test Category 2");
            var resultCategoryNotFound = await categoryService.GetCategoryByNameAsync("Non-existent Category");

            // Assert
            Assert.NotNull(resultCategory1);
            Assert.AreEqual(category1.Id, resultCategory1.Id);

            Assert.NotNull(resultCategory2);
            Assert.AreEqual(category2.Id, resultCategory2.Id);

            Assert.Null(resultCategoryNotFound);
        }
    }
}
