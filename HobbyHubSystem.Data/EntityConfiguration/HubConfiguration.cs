using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HubConfiguration : IEntityTypeConfiguration<Hub>
    {
        public void Configure(EntityTypeBuilder<Hub> builder)
        {

            builder
               .HasOne(e => e.Hobby)
               .WithOne(e => e.Hub)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
