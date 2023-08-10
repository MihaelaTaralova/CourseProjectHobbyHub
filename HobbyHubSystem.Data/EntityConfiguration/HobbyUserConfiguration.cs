using HobbyBubSystem.Data.Models.Account;
using HobbyHubSystem.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HobbyUserConfiguration : IEntityTypeConfiguration<HobbyUser>
    {

        //private readonly UserManager<HobbyUser> _userManager;
        //private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        //public HobbyUserConfiguration(UserManager<HobbyUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}

        public void Configure(EntityTypeBuilder<HobbyUser> builder)
        {
            builder
                   .Property(a => a.RegisteredOn)
                   .HasDefaultValueSql("GETDATE()");


            //var hasher = new PasswordHasher<HobbyUser>();

            //var moderator = new HobbyUser
            //{
            //    FirstName = "Mihaela",
            //    LastName = "Mihael4ov",
            //    UserName = "Mihaela Mihael4ov",
            //    Gender = "female",
            //    RegisteredOn = DateTime.UtcNow,
            //    NormalizedUserName = "MIHAELA MIHAEL4OV",
            //    Email = "mihaela@test.com",
            //    NormalizedEmail = "MIHAELA@TEST.COM",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "Moderator123"),
            //    SecurityStamp = string.Empty,
            //};

            //var admin = new HobbyUser
            //{
            //    FirstName = "Samuil",
            //    LastName = "Sam4ov",
            //    UserName = "Samuil Sam4ov",
            //    Gender = "male",
            //    RegisteredOn = DateTime.UtcNow,
            //    NormalizedUserName = "SAMUIL SAM4OV",
            //    Email = "sami@test.com",
            //    NormalizedEmail = "SAMI@TEST.COM",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "Admin123"),
            //    SecurityStamp = string.Empty,
            //};
            //builder.HasData(admin, moderator);


            //// Create roles if they don't exist
            //if (!_roleManager.RoleExistsAsync(RoleConstants.Administrator).Result)
            //{
            //    _roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.Administrator)).Wait();
            //}

            //if (!_roleManager.RoleExistsAsync(RoleConstants.Moderator).Result)
            //{
            //    _roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.Moderator)).Wait();
            //}

            //// Assign roles to users
            //_userManager.AddToRoleAsync(admin, RoleConstants.Administrator).Wait();
            //_userManager.AddToRoleAsync(moderator, RoleConstants.Moderator).Wait();
        }
    }
}
