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

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<Hobby> Hobbies { get; set; }
    }
}
