using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using static HobbyHubSystem.Tests.DatabaseSeeder;


namespace HobbyHubSystem.Tests
{
    public class ArticleServiceTests
    {
        private DbContextOptions<HobbyHubDbContext> options;
        private HobbyHubDbContext dbContext;
        private IArticleService articleService;
        private string tempImageDirectory;

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

            this.articleService = new ArticleService(dbContext);
            var mockImageService = new MockImageService();


        }

        [TearDown]
        public void CleanUp()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddArticleAsync_ShouldAddArticleToDatabase()
        {
            //Arrange
            var articleViewModel = new AddArticleViewModel()
            {
                Id = 12,
                Title = "Test Article",
                Content = "Test Article Description",
                PublishDate = DateTime.UtcNow,
                HubId = 1
            };
            var authorId = Guid.NewGuid();

            //Act
            await articleService.AddArticleAsync(articleViewModel, authorId);

            //Assert
            var addedArticle = dbContext.Articles.FirstOrDefault(a => a.Title == "Test Article");
            Assert.IsNotNull(addedArticle);
            Assert.AreEqual("Test Article", addedArticle.Title);
            Assert.AreEqual("Test Article Description", addedArticle.Content);
            Assert.AreEqual(1, addedArticle.HubId);
            Assert.That(authorId, Is.EqualTo(addedArticle.AuthorId));
        }

        [Test]
        public async Task ApproveArticleAsync_WithExistingArticle_ShouldSetIsApprovedToTrue()
        {
            //Arrange
            var article = dbContext.Articles.First();
            var articleId = article.Id;

            //Act
            await articleService.ApproveArticleAsync(articleId);

            //Assert
            var updatedArticle = dbContext.Articles.Find(articleId);
            Assert.True(updatedArticle.IsApproved);
        }

        [Test]
        public async Task DeleteArticle_ShouldSetIsActiveToFalse()
        {
            //Arrange
            var currentArticle = dbContext.Articles.First();
            var articleId = currentArticle.Id;

            //Act
            await articleService.DeleteArticle(articleId);

            //Assert
            var deletedArticle = dbContext.Articles.Find(articleId);
            Assert.NotNull(deletedArticle);
            Assert.IsFalse(deletedArticle.IsActive);
        }

        [Test]
        public async Task DeleteArticle_NonExistentArticle_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistentArticleId = 333;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await articleService.DeleteArticle(nonExistentArticleId));
        }

        [Test]
        public async Task EditArticle_ShouldUpdateArticleFields()
        {
            //Arrange
            var article = dbContext.Articles.First();
            var articleId = article.Id;

            var newArticleTitle = "Edited Article";
            var newArticleContent = "Edited content";

            var editModel = new EditArticleViewModel()
            {
                Title = newArticleTitle,
                Content = newArticleContent
            };

            //Act
            await articleService.EditArticle(articleId, editModel);

            //Assert
            var updatedArticle = dbContext.Articles.Find(articleId);
            Assert.AreEqual(newArticleTitle, updatedArticle.Title);
            Assert.AreEqual(newArticleContent, updatedArticle.Content);
        }

        [Test]
        public async Task GetAllArticleAsync_ShouldReturnActiveArticles()
        {
            // Arrange
            var hub = dbContext.Hubs.FirstOrDefault();
            if (hub == null)
            {
                Assert.Fail("No hubs found in the database.");
            }
            var hubId = hub.Id;

            var user = dbContext.Users.FirstOrDefault();
            if (user == null)
            {
                Assert.Fail("No user found in the database.");
            }
            var userId = user.Id;

            var mockArticle1 = new Article()
            {
                Id = 1,
                Title = "Test1",
                Content = "Test of test 1",
                HubId = hubId,
                IsActive = true,
                IsApproved = true,
                AuthorId = userId,
                PublishDate = DateTime.UtcNow,
            };

            var mockArticle2 = new Article()
            {
                Id = 2,
                Title = "Test2",
                Content = "Test of test 2",
                HubId = hubId,
                IsActive = true,
                IsApproved = true,
                AuthorId = userId,
                PublishDate = DateTime.UtcNow,
            };

            var mockArticle3 = new Article()
            {
                Id = 3,
                Title = "Test3",
                Content = "Test of test 3",
                HubId = hubId,
                IsActive = false,
                IsApproved = false,
                AuthorId = userId,
                PublishDate = DateTime.UtcNow,
            };

            dbContext.Articles.AddRange(mockArticle1, mockArticle2, mockArticle3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await articleService.GetAllArticlesAsync(hubId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetArticleWithAuthorAsync_ShouldReturnApprovedArticleWithAuthor()
        {
            // Arrange
            var hub = dbContext.Hubs.FirstOrDefault();
            if (hub == null)
            {
                Assert.Fail("No hubs found in the database.");
            }
            var hubId = hub.Id;

            var user = dbContext.Users.FirstOrDefault();
            if (user == null)
            {
                Assert.Fail("No user found in the database.");
            }
            var userId = user.Id;

            var mockArticle = new Article
            {
                Id = 1,
                Title = "Test Article",
                Content = "Test content",
                AuthorId = userId,
                IsActive = true,
                IsApproved = true,
                HubId = hubId,
                PublishDate = DateTime.UtcNow
            };

            dbContext.Articles.Add(mockArticle);
            await dbContext.SaveChangesAsync();

            // Act
            var articleViewModel = await articleService.GetArticleWithAuthorAsync(mockArticle.Id);

            // Assert
            Assert.NotNull(articleViewModel);
            Assert.AreEqual(mockArticle.Id, articleViewModel.Id);
            Assert.AreEqual(mockArticle.Title, articleViewModel.Title);
            Assert.AreEqual(mockArticle.Content, articleViewModel.Content);
            Assert.AreEqual(user.UserName, articleViewModel.AuthorName);
        }

        [Test]
        public async Task GetPendingArticlesAsync_ShouldReturnPendingArticles()
        {
            // Arrange
            var user = dbContext.Users.FirstOrDefault();
            if (user == null)
            {
                Assert.Fail("No user found in the database.");
            }
            var userId = user.Id;

            var hub = dbContext.Hubs.FirstOrDefault();
            if (hub == null)
            {
                Assert.Fail("No hubs found in the database.");
            }
            var hubId = hub.Id;

            var mockPendingArticles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "Pending Article 1",
                    Content = "Content for pending article 1",
                    IsApproved = false,
                    IsActive = true,
                    AuthorId = userId,
                    HubId = hubId,
                    PublishDate = DateTime.UtcNow
            },
                    new Article
                    {
                        Id = 2,
                        Title = "Pending Article 2",
                        Content = "Content for pending article 2",
                        IsApproved = false,
                        IsActive = true,
                        AuthorId = userId,
                        HubId = hubId,
                        PublishDate = DateTime.UtcNow
                    }
            };
            mockPendingArticles = mockPendingArticles.OrderBy(a => a.Id).ToList();

            dbContext.Articles.AddRange(mockPendingArticles);
            await dbContext.SaveChangesAsync();

            // Act
            var pendingArticlesViewModel = await articleService.GetPendingArticlesAsync();

            // Assert
            Assert.NotNull(pendingArticlesViewModel);
            Assert.AreEqual(2, pendingArticlesViewModel.PendingArticles.Count);

            var firstPendingArticle = pendingArticlesViewModel.PendingArticles.FirstOrDefault();
            Assert.NotNull(firstPendingArticle);
            Assert.AreEqual(mockPendingArticles[1].Id, firstPendingArticle.Id);
            Assert.AreEqual(mockPendingArticles[1].Title, firstPendingArticle.Title);
            Assert.AreEqual(mockPendingArticles[1].Content, firstPendingArticle.Content);
            Assert.AreEqual(mockPendingArticles[1].PublishDate, firstPendingArticle.PublishDate);
            Assert.AreEqual(mockPendingArticles[1].AuthorId, firstPendingArticle.AuthorId);
            Assert.AreEqual(mockPendingArticles[1].HubId, firstPendingArticle.HubId);
        }

    }
}
