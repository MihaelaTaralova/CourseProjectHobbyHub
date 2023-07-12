using HobbyBubSystem.Data.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyBubSystem.Data.Models
{
    public class HobbyUserHub
    {
        [ForeignKey(nameof(HobbyUser))]
        public Guid HobbyUserId { get; set; }

        public HobbyUser? HobbyUser { get; set; }

        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }

        public Hub Hub { get; set; } = null!;
    }
}
