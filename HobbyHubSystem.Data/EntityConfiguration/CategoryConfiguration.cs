using HobbyBubSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbyHubSystem.Data.EntityConfiguration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        public Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Sports",
                ImageUrl = "https://penaltyfile.com/wp-content/uploads/2020/06/different-types-of-sports-June32020-1-min.jpg",
              
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
