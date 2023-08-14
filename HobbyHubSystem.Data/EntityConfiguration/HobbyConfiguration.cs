using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.Mime.MediaTypeNames;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
        public HobbyConfiguration()
        {
            
        }
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
