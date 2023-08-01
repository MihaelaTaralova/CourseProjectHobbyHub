using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HobbyUserConfiguration : IEntityTypeConfiguration<HobbyUser>
    {
        public void Configure(EntityTypeBuilder<HobbyUser> builder)
        {
            builder
                   .Property(a => a.RegisteredOn)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
