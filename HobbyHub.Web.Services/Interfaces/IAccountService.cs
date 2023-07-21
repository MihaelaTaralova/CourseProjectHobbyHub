using HobbyBubSystem.Data.Models.Account;
using HobbyHubSystem.Web.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUser(RegisterViewModel model);

        Task<SignInResult> Login(LoginViewModel loginViewModel);

    }
}
