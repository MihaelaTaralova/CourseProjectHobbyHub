﻿using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using Microsoft.EntityFrameworkCore;

namespace HobbyHubSystem.Tests
{
    public static class DatabaseSeeder
    {
        private static Hobby hobby;
        private static Category category;

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
        }
    }
}
