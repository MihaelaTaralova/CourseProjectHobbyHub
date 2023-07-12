using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
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

            //builder.HasData(this.GenerateHobbies());

        }

        //public Hobby[] GenerateHobbies() 
        //{
        //ICollection<Hobby> hobbies;

        //    Hobby hobby;

        //    hobby = new Hobby()
        //    {

        //    };

        //    hobbies.Add(hobby);

        //    return hobbies.ToArray();
        //}

    }
}
