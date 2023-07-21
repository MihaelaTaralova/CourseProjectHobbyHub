using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Web.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using HobbyHubSystem.Web.ViewModels.Account;

namespace HobbyHub.Web.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<HobbyUser> userManager;
        private readonly SignInManager<HobbyUser> signInManager;
        private readonly IImageService imageService;


        public AccountService(UserManager<HobbyUser> _userManager, 
            SignInManager<HobbyUser> _signInManager, 
            IImageService _imageService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.imageService = _imageService;
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel model)
        {
            var imageUrl = await imageService.SaveImage(model.ImageUrl);

            var user = new HobbyUser()
            {
                Email = model.Email,
                UserName = model.FirstName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                ImageUrl = imageUrl
        };
            var result = await userManager.CreateAsync(user, model.Password);

            return result;

        }

        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {
            var user = await userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                return result;
            }

            return null;
        }
    }
}
