using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HobbyUserHubConfiguration : IEntityTypeConfiguration<HobbyUserHub>
    {
        public void Configure(EntityTypeBuilder<HobbyUserHub> builder)
        {
            builder
        .HasKey(e => new { e.HobbyUserId, e.HubId });

            builder
                .HasOne(e => e.HobbyUser)
                .WithMany(e => e.HobbyUsersHubs)
                .HasForeignKey(e => e.HobbyUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Hub)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.HubId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
