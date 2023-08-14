using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class HubConfiguration : IEntityTypeConfiguration<Hub>
    {
        public HubConfiguration()
        {
           
        }

        public void Configure(EntityTypeBuilder<Hub> builder)
        {

            builder
               .HasOne(e => e.Hobby)
               .WithOne(e => e.Hub)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateHubs());
        }

        public Hub[] GenerateHubs()
        {
            ICollection<Hub> hubs = new HashSet<Hub>();

            Hub hub;

            hub = new Hub()
            {
                Id = 1,
                Name = "Water skiing",
                About = "This hub belongs to hobby Water skiing and all articles, events and discussions are connected with Water skiing",
                CreatorId = Guid.Parse("c5e2081c-5052-4162-b0d7-1920163d6b9d"),
                HobbyId = 1
            };

            hubs.Add(hub);

            hub = new Hub()
            {
                Id = 2,
                Name = "Reiki",
                About = "This hub belongs to hobby Reiki and all articles, events and discussions are connected with Reiki",
                CreatorId = Guid.Parse("2a29f172-6978-420f-a929-ca5678254935"),
                HobbyId = 2
            };

            hubs.Add(hub);

            return hubs.ToArray();

        }

    }
    }
