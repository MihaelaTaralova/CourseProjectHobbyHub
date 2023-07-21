using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Common;
using HobbyHubSystem.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<HobbyUser> userManager;
        private readonly SignInManager<HobbyUser> signInManager;
        private readonly IAccountService accountService;
        private readonly IUserManagementService userManagementService;

        public AccountController(UserManager<HobbyUser> _userManager,
            SignInManager<HobbyUser> _signInManager,
            IAccountService _accountService,
            IUserManagementService _userManagementService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.accountService = _accountService;
            this.userManagementService = _userManagementService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var result = await accountService.RegisterUser(registerViewModel);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(registerViewModel.Email);
                //await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);

            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await accountService.Login(loginViewModel);

            if (result != null && result.Succeeded)
            {
                if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login");

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await userManagementService.CreateRoles();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            await userManagementService.AddUsersToRoles();
            return RedirectToAction("Index", "Home");
        }
    }
}
