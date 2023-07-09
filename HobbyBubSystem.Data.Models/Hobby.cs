using HobbyBubSystem.Data.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HobbyHubSystem.Common.EntityValidationConstants.Hobby;

namespace HobbyBubSystem.Data.Models
{
    public class Hobby
    {
        public Hobby()
        {
            this.CreatorId = Guid.NewGuid();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public bool IsApproved { get; set; } // Флаг, указващ дали хобито е одобрено от модератора

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }

        public virtual Hub Hub { get; set; } = null!;

    }
}
