using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Data;
using Microsoft.EntityFrameworkCore;

namespace HobbyHubSystem.Tests
{
    public static class DatabaseSeeder
    {
        private static Hobby hobby;
        private static Category category;
        private static Event testEvent;
        private static Hub hub;
        private static Article article;
        private static HobbyUser user;

        private static int eventId = 14;
        private static int hubId = 20;

        public static void SeedDatabase(HobbyHubDbContext dbContext)
        {
            hobby = new Hobby()
            {
                Name = "Hobby 1",
                Description = "Description for Hobby 1",
                CreatorId = Guid.NewGuid(),
                CategoryId = 11,
                IsApproved = true,
                IsActive = true,
                ImageUrl = "image1.jpg",
                HubId = 20
            };


            dbContext.Hobbies.Add(hobby);

            hobby = new Hobby
            {
                Name = "Hobby 2",
                Description = "Description for Hobby 2",
                CreatorId = Guid.NewGuid(),
                CategoryId = 10,
                IsApproved = true,
                IsActive = true,
                ImageUrl = "image2.jpg",
                HubId = 21
            };


            dbContext.Hobbies.Add(hobby);
            dbContext.SaveChanges();


            // Seed Categories
            category = new Category
            {
                Name = "Category 1",
                IsActive = true,
                ImageUrl = "category-image1.jpg"
            };
            dbContext.Categories.Add(category);

            category = new Category
            {
                Name = "Category 2",
                IsActive = true,
                ImageUrl = "category-image2.jpg"
            };
            dbContext.Categories.Add(category);

            dbContext.SaveChanges();


            //Seed events
            testEvent = new Event()
            {
                Id = eventId,
                Title = "Sample Event",
                Description = "Description for the event",
                CreatorId = Guid.NewGuid(),
                Location = "Sample Location",
                DateOfEvent = DateTime.UtcNow.AddDays(7),
                HubId = hubId
            };

            dbContext.Events.Add(testEvent);

            dbContext.SaveChanges();


            //Seed hub
            hub = new Hub()
            {
                Id = hubId,
                Name = "Test hub",
                About = "About to test hub",
                CreatorId = Guid.NewGuid(),
                HobbyId = hobby.Id
            };
            dbContext.Hubs.Add(hub);
            dbContext.SaveChanges();


            //Seed article
            article = new Article()
            {
                Id = 20,
                Title = "Sample article",
                Content = "Content of the article",
                AuthorId = Guid.NewGuid(),
                IsApproved = true,
                IsActive = true,
                PublishDate = DateTime.UtcNow,
                HubId = hubId
            };
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();

            //Seed user
            user = new HobbyUser()
            {
                FirstName = "Test",
                LastName = "Testov",
                Gender = "male",
                RegisteredOn = DateTime.UtcNow,
                ImageUrl = "image3.jpg"
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
    }
    }
    }


