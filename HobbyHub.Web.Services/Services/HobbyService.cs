﻿using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Admin;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Web.Services.Services
{
    public class HobbyService : IHobbyService
    {
        private readonly HobbyHubDbContext dbContext;
        private readonly IImageService imageService;

        public HobbyService(HobbyHubDbContext _dbContext,
            IImageService _imageService)
        {
            this.dbContext = _dbContext;
            this.imageService = _imageService;
        }

        public async Task AddHobbyAsync(AddHobbyViewModel hobbyViewModel, Guid userId)
        {
            var imageUrl = await imageService.SaveImage(hobbyViewModel.ImageUrl);

            var hobby = new Hobby()
            {
                Name = hobbyViewModel.Name,
                Description = hobbyViewModel.Description,
                IsActive = true,
                IsApproved = true,
                ImageUrl = imageUrl,
                CreatorId = userId,
                CategoryId = hobbyViewModel.CategoryId
            };

            await dbContext.Hobbies.AddAsync(hobby);
            await dbContext.SaveChangesAsync();
        }

        public async Task ApproveHobbyAsync(int hobbyId)
        {
            var hobby = await dbContext.Hobbies.FindAsync(hobbyId);
            if (hobby == null)
            {
                throw new ArgumentException("Hobby not found");
            }

            hobby.IsApproved = true;
            dbContext.Hobbies.Update(hobby);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteHobbyAsync(int categoryId)
        {
            var hobby = await dbContext.Hobbies.FindAsync(categoryId);
            if (hobby == null)
            {
                throw new ArgumentException("Hobby not found");
            }

            hobby.IsActive = false;
            dbContext.Hobbies.Update(hobby);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditHobbyAsync(int categoryId, AddHobbyViewModel model)
        {
            var hobby = await dbContext.Hobbies.FindAsync(categoryId);
            if (hobby == null) 
            {
                throw new ArgumentException("Hobby not found");
            }

            hobby.Name = model.Name;
            hobby.Description = model.Description;
            hobby.IsApproved = true;
            hobby.IsActive = true;

            if (model.ImageUrl != null)
            {
                hobby.ImageUrl = await imageService.SaveImage(model.ImageUrl);
            }

            dbContext.Hobbies.Update(hobby);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Hobby>> GetAllHobbiesAsync()
        {
            return await dbContext.Hobbies.Where(h => h.IsActive).ToListAsync();
        }

        public async Task<List<Hobby>> GetHobbiesByCategoryId(int categoryId)
        {
            return await dbContext.Hobbies.Where(c => c.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Hobby> GetHobbyByIdAsync(int hobbyId)
        {
            return await dbContext.Hobbies.FindAsync(hobbyId);
        }

        public async Task<PendingHobbiesViewModel> GetPendingHobbiesAsync()
        {
            var pendingHobbies = await dbContext.Hobbies.Where(h => !h.IsApproved).ToListAsync();
            var viewModel = new PendingHobbiesViewModel
            {
                PendingHobbies = pendingHobbies.Select(h => new HobbyViewModel
                {
                    HobbyId = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    CategoryId = h.CategoryId,
                    HubId = h.HubId,
                }).ToList()
            };
            return viewModel;
        }
    }
}