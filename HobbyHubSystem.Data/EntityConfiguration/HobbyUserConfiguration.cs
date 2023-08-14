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

            builder.HasData(this.GenerateHobbyUsers());

                      
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

        public HobbyUser[] GenerateHobbyUsers() 
        {
            ICollection<HobbyUser> hobbyUsers = new HashSet<HobbyUser>();

            HobbyUser hobbyUser;

            var hasher = new PasswordHasher<HobbyUser>();

            hobbyUser = new HobbyUser()           
            {
                Id = Guid.Parse("c5e2081c-5052-4162-b0d7-1920163d6b9d"),
                FirstName = "Mihaela",
                LastName = "Mihael4ov",
                UserName = "Mihaela Mihael4ov",
                Gender = "female",
                RegisteredOn = DateTime.UtcNow,
                NormalizedUserName = "MIHAELA MIHAEL4OV",
                Email = "mihaela@abv.bg",
                NormalizedEmail = "MIHAELA@ABV.BG",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "mihaela123"),
                SecurityStamp = string.Empty,
                ImageUrl = "https://www.taylorherring.com/wp-content/uploads/2015/03/Archetypal-Female-Face-of-Beauty-embargoed-to-00.01hrs-30.03.15.jpg"
            };

            hobbyUsers.Add(hobbyUser);

            hobbyUser = new HobbyUser()
            {
                Id = Guid.Parse("2a29f172-6978-420f-a929-ca5678254935"),
                FirstName = "Sami",
                LastName = "Sam4ov",
                UserName = "Sami Sam4ov",
                Gender = "male",
                RegisteredOn = DateTime.UtcNow,
                NormalizedUserName = "SAMI SAM4OV",
                Email = "sami@abv.bg",
                NormalizedEmail = "SAMI@ABV.BG",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "sami123"),
                SecurityStamp = string.Empty,
                ImageUrl = "https://www.taylorherring.com/wp-content/uploads/2015/03/Archetypal-Male-Face-of-Beauty-embargoed-to-00.01hrs-30.03.15.jpg"
            };

            hobbyUsers.Add(hobbyUser);

            return hobbyUsers.ToArray();
        }
    }
}
