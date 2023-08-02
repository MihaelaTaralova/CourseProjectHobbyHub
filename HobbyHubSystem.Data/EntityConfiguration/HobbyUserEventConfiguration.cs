using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HobbyUserEventConfiguration : IEntityTypeConfiguration<HobbyUserEvent>
    {
        public void Configure(EntityTypeBuilder<HobbyUserEvent> builder)
        {
            builder
        .HasKey(e => new { e.HobbyUserId, e.EventId });

            builder
                .HasOne(e => e.HobbyUser)
                .WithMany(e => e.HobbyUserEvents)
                .HasForeignKey(e => e.HobbyUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Event)
                .WithMany(e => e.HobbyUsers)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
