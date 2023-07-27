using System.ComponentModel.DataAnnotations.Schema;
using HobbyBubSystem.Data.Models.Account;


namespace HobbyBubSystem.Data.Models
{
    public class HobbyUserEvent
    {
        [ForeignKey(nameof(HobbyUser))]
        public Guid HobbyUserId { get; set; }

        public HobbyUser? HobbyUser { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}
