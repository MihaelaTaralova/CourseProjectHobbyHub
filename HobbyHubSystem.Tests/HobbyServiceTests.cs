using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static HobbyHubSystem.Tests.DatabaseSeeder;

namespace HobbyHubSystem.Tests;

public class HobbyServiceTests
{
    private DbContextOptions<HobbyHubDbContext> options;
    private HobbyHubDbContext dbContext;
    private string tempImageDirectory;
    private IHobbyService hobbyService;

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
        this.hobbyService = new HobbyService(dbContext, mockImageService);

    }

    [TearDown]
    public void CleanUp()
    {
        dbContext.Dispose();
        Directory.Delete(tempImageDirectory, true);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public async Task AddHobbyAsync_WithValidData_ShouldAddHobbyAndHub()
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
        var hobbyViewModel = new AddHobbyViewModel
        {
            Name = "Test Hobby",
            Description = "Test Description",
            ImageUrl = formFile,
            HubId = 21,
            CategoryId = 10,
        };
        var userId = Guid.NewGuid();

        // Act
        await hobbyService.AddHobbyAsync(hobbyViewModel, userId);

        // Assert
        var addedHobby = dbContext.Hobbies.FirstOrDefault(h => h.Name == hobbyViewModel.Name);
        Assert.NotNull(addedHobby);

        var addedHub = dbContext.Hubs.FirstOrDefault(h => h.HobbyId == addedHobby.Id);
        Assert.NotNull(addedHub);
    }

    [Test]
    public async Task ApproveHobbyAsync_WithExistingHobby_ShouldSetIsApprovedToTrue()
    {
        // Arrange
        var hobby = dbContext.Hobbies.First(); 
        var hobbyId = hobby.Id;

        // Act
        await hobbyService.ApproveHobbyAsync(hobbyId);

        // Assert
        var updatedHobby = dbContext.Hobbies.Find(hobbyId);
        Assert.True(updatedHobby.IsApproved);
    }

    [Test]
    public void ApproveHobbyAsync_WithNonExistingHobby_ShouldThrowException()
    {
        // Arrange
        var nonExistingHobbyId = -1;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await hobbyService.ApproveHobbyAsync(nonExistingHobbyId));
    }

    [Test]
    public async Task DeleteHobbyAsync_WithExistingHobby_ShouldDeactivateHobby()
    {
        // Arrange
        var hobby = dbContext.Hobbies.First();
        var hobbyId = hobby.Id;

        // Act
        await hobbyService.DeleteHobbyAsync(hobbyId);

        // Assert
        var updatedHobby = dbContext.Hobbies.Find(hobbyId);
        Assert.False(updatedHobby.IsActive);
    }

    [Test]
    public void DeleteHobbyAsync_WithNonExistingHobby_ShouldThrowException()
    {
        // Arrange
        var nonExistingHobbyId = -1;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await hobbyService.DeleteHobbyAsync(nonExistingHobbyId));
    }

    [Test]
    public async Task EditHobbyAsync_WithExistingHobby_ShouldUpdateHobbyDetails()
    {
        // Arrange
        var hobby = dbContext.Hobbies.First(); // Assuming there's at least one hobby in the database
        var hobbyId = hobby.Id;

        var newHobbyName = "New Hobby Name";
        var newHobbyDescription = "New Hobby Description";
        var editModel = new EditHobbyViewModel
        {
            Name = newHobbyName,
            Description = newHobbyDescription
        };

        // Act
        await hobbyService.EditHobbyAsync(hobbyId, editModel);

        // Assert
        var updatedHobby = dbContext.Hobbies.Find(hobbyId);
        Assert.AreEqual(newHobbyName, updatedHobby.Name);
        Assert.AreEqual(newHobbyDescription, updatedHobby.Description);
    }

    [Test]
    public void EditHobbyAsync_WithNonExistingHobby_ShouldThrowException()
    {
        // Arrange
        var nonExistingHobbyId = -1;
        var editModel = new EditHobbyViewModel();

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await hobbyService.EditHobbyAsync(nonExistingHobbyId, editModel));
    }

    [Test]
    public async Task GetAllHobbiesAsync_ShouldReturnActiveHobbies()
    {
        // Act
        var result = await hobbyService.GetAllHobbiesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);  
    }

    [Test]
    public async Task GetHobbiesByCategoryId_ShouldReturnHobbiesWithMatchingCategoryId()
    {
        // Arrange
        int categoryIdToSearch = 11; 
        var expectedHobbiesCount = 1; 

        // Act
        var result = await hobbyService.GetHobbiesByCategoryId(categoryIdToSearch);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedHobbiesCount, result.Count);
        Assert.IsTrue(result.All(h => h.CategoryId == categoryIdToSearch));
    }

    [Test]
    public async Task GetHobbyByIdAsync_ShouldReturnHobbyWithMatchingId()
    {
        // Arrange
        int hobbyIdToSearch = 1; 

        // Act
        var result = await hobbyService.GetHobbyByIdAsync(hobbyIdToSearch);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(hobbyIdToSearch, result.Id);
    }

    [Test]
    public async Task GetPendingHobbiesAsync_ShouldReturnPendingHobbiesViewModelWithCorrectData()
    {
        // Arrange
        var pendingHobby1 = new Hobby
        {
            Id = 1000,
            IsApproved = false,
            Name = "Pending Hobby 1",
            Description = "Description for Pending Hobby 1",
            ImageUrl = "pending-image1.jpg"
        };

        var pendingHobby2 = new Hobby
        {
            Id = 2002,
            IsApproved = false,
            Name = "Pending Hobby 2",
            Description = "Description for Pending Hobby 2",
            ImageUrl = "pending-image2.jpg"
        };

        dbContext.Hobbies.AddRange(pendingHobby1, pendingHobby2);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await hobbyService.GetPendingHobbiesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.PendingHobbies);
        Assert.AreEqual(2, result.PendingHobbies.Count);

      
        var firstPendingHobby = result.PendingHobbies[1];
        Assert.AreEqual(1000, firstPendingHobby.HobbyId);
        

        
        var secondPendingHobby = result.PendingHobbies[0];
        Assert.AreEqual(2002, secondPendingHobby.HobbyId);
     
    }
}

