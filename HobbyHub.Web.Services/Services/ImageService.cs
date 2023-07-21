using HobbyHub.Web.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HobbyHub.Web.Services.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public ImageService(IWebHostEnvironment _hostEnvironment)
        {
            hostEnvironment = _hostEnvironment;
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            // Генериране на уникално име за изображението
            var uniqueFileName = Guid.NewGuid().ToString() + image.FileName;

            // Път до папката, в която ще се съхраняват изображенията
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, "images");

            // Път до файла на изображението
            var filePath = Path.Combine(imagePath, uniqueFileName);

            // Съхраняване на изображението на диска
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Генериране на URL към изображението
            var imageUrl = "/images/" + uniqueFileName;

            return imageUrl;
        }

        public bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            string[] allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            return allowedTypes.Contains(file.ContentType);
        }

    }
}

