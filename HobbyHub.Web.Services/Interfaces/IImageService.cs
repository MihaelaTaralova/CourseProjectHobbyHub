using Microsoft.AspNetCore.Http;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile image);

        bool IsValidImage(IFormFile file);
    }
}
