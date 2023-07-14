using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.HobbyUser;

namespace HobbyBubSystem.Data.Models.Account
{
    public class HobbyUser : IdentityUser<Guid>
    {
        public HobbyUser()
        {
            this.Id = Guid.NewGuid();
            this.Events = new HashSet<Event>();
            this.Questions = new HashSet<Question>();
            this.Answers = new HashSet<Answer>();
            this.HobbyUsersHubs = new HashSet<HobbyUserHub>();
        }

        [Required]
        [MaxLength(FirstNameMax)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMax)]
        public string LastName { get; set; } = null!;

        [Required]
        public string Gender { get; set; } = null!;

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;
      
        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<HobbyUserHub> HobbyUsersHubs { get; set;}
    }
}
