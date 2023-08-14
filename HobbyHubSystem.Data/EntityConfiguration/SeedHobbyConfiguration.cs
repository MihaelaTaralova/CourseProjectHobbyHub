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
    public class SeedHobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
        public void Configure(EntityTypeBuilder<Hobby> builder)
        {
            builder.HasData(this.GenerateHobbies());
        }

        public Hobby[] GenerateHobbies()
        {
            ICollection<Hobby> hobbies = new HashSet<Hobby>();

            Hobby hobby;

            hobby = new Hobby()
            {
                Id = 1,
                Name = "Water skiing",
                Description = "Water skiing (also waterskiing or water-skiing) is a surface water sport in which an individual is pulled behind a boat or a cable ski installation over a body of water, skimming the surface on two skis or one ski.",
                IsActive = true,
                IsApproved = true,
                ImageUrl = "https://worldwaterskiers.com/wp-content/uploads/2019/06/im-waterskiing.jpg",
                HubId = 1,
                CreatorId = Guid.Parse("c5e2081c-5052-4162-b0d7-1920163d6b9d"),
                CategoryId = 1

            };
            hobbies.Add(hobby);

            hobby = new Hobby()
            {
                Id = 2,
                Name = "Reiki",
                Description = "Reiki is a form of energy healing. Its roots can be traced back to the 1920’s in Japan. Pronounced “ray-kee”, Reiki can be roughly translated to mean “Universal Life Force” where Rei means Universal and Ki is Life force. ",
                IsActive = true,
                IsApproved = true,
                ImageUrl = "https://www.tododisca.com/wp-content/uploads/2020/08/sesion-de-reiki-2-1.jpg",
                HubId = 2,
                CreatorId = Guid.Parse("2a29f172-6978-420f-a929-ca5678254935"),
                CategoryId = 2
            };

            hobbies.Add(hobby);

            return hobbies.ToArray();
        }
    }
    }

