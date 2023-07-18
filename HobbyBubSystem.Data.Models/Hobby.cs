using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
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

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("name of the hobby")]
        [Required]
        [MaxLength(NameMax)]
        public string Name { get; set; } = null!;

        [Comment("text with detailed description about the hobby")]
        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Comment("person who descibed the hobby in the system")]
        [Required]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [Comment("category to which the hobby belongs")]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        [Comment("before release the hobby should be approved by admin or moderator")]
        public bool IsApproved { get; set; }

        [Comment("when it is false - the hobby is deleted")]
        public bool IsActive { get; set; }

        [Comment("picture of the hobby")]
        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Comment("this is the group which belongs to the hobby")]
        [Required]
        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }

        public virtual Hub Hub { get; set; } = null!;

    }
}
