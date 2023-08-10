using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
        private readonly UserManager<HobbyUser> _userManager;

        public void Configure(EntityTypeBuilder<Hobby> builder)
        {
            builder
               .Property(a => a.IsApproved)
               .HasDefaultValue(false);

            builder
               .HasOne(e => e.Category)
               .WithMany(h => h.Hobbies)
               .HasForeignKey(q => q.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Hub)
                .WithOne(h => h.Hobby)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
