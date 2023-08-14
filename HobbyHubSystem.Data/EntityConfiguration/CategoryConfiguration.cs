using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
              .HasMany(h => h.Hobbies)
              .WithOne(h => h.Category)
              .HasForeignKey(h => h.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateCategories());
        }

        public Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Water",
                ImageUrl = "https://dawnmagazines.com/wp-content/uploads/2020/10/Water-Sports.jpg",
              
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Spiritual and Mental",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e1/Pilates_Moscow.jpg"
            };

            categories.Add(category);

            return categories.ToArray();
        }
    }
}
