using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Common;
using Microsoft.AspNetCore.Identity;
using HobbyBubSystem.Data.Models.Account;

namespace HobbyHub.Web.Services.Services
{
    public class UserManagementService : IUserManagementService
    {

        private readonly UserManager<HobbyUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserManagementService(UserManager<HobbyUser> userManager,
            RoleManager<IdentityRole<Guid>> _roleManager)
        {
            this.userManager = userManager;
            this.roleManager = _roleManager;
        }
        public async Task CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.Administrator));
            await roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.Moderator));
        }

        public async Task AddUsersToRoles()
        {
            string email1 = "mihaela@abv.bg";
            string email2 = "sami@abv.bg";

            var user = await userManager.FindByEmailAsync(email1);
            var user1 = await userManager.FindByEmailAsync(email2);

            await userManager.AddToRolesAsync(user, new string[] { RoleConstants.Administrator, RoleConstants.Moderator });
            await userManager.AddToRoleAsync(user1, RoleConstants.Moderator);
        }
    }
}

