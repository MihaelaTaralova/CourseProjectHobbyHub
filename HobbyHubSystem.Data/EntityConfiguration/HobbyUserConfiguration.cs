using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
