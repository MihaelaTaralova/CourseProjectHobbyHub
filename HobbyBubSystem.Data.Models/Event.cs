﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Event;
using HobbyBubSystem.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace HobbyBubSystem.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.CreatorId = Guid.NewGuid();
            this.HobbyUsers = new List<HobbyUserEvent>();
        }

        [Comment("unique identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("name of the event")]
        [Required]
        [MaxLength(TitleMax)]
        public string Title { get; set; } = null!;

        [Comment("user who enters the event in the system")]
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public virtual HobbyUser Creator { get; set; } = null!;

        [Comment("details of the event")]
        [Required]
        [MaxLength(DescriptionMax)]
        public string Description { get; set; } = null!;

        [Comment("date on which the event will happen")]
        [Required]
        public DateTime DateOfEvent { get; set; }

        [Comment("location where the event will happen")]
        [Required]
        [MaxLength(LocationMax)]
        public string Location { get; set; } = null!;

        [Comment("hub where the event belongs")]
        [ForeignKey(nameof(Hub))]
        public int HubId { get; set; }

        public virtual Hub Hub { get; set; } = null!;

        [Comment("when it is false - the event is deleted")]
        public bool IsActive { get; set; } = true;

        [Comment("people who will attend the event")]
        public virtual ICollection<HobbyUserEvent> HobbyUsers { get; set; }
    }
}
