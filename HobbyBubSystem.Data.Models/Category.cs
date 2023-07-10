using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Category;


namespace HobbyBubSystem.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Hobbies = new HashSet<Hobby>();
        }

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("name of the category")]
        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Comment("picture of the category")]
        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Comment("collection with the hobbies for the category")]
        public virtual ICollection<Hobby> Hobbies { get; set; }
    }
}
