using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Tests
{
    public class MockImageService : IImageService
    {
        public Task<string> SaveImage(IFormFile image)
        {
            return Task.FromResult("/mock-images/image.jpg");
        }

        public bool IsValidImage(IFormFile file)
        {
            return true;
        }
    }
}
