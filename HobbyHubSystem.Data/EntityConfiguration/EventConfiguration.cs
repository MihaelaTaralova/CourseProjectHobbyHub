using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {

            builder
                .HasOne(e => e.Creator)
                .WithMany(c => c.Events)
                .HasForeignKey(q => q.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Hub)
              .WithMany(h => h.Events)
              .HasForeignKey(q => q.HubId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
