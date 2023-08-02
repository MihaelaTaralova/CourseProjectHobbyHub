using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static HobbyHubSystem.Common.EntityValidationConstants.Event;

namespace HobbyHubSystem.Web.ViewModels.Event
{
    public class EditEventViewModel
    {
        [Required]
        [StringLength(TitleMax), MinLength(TileMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMax), MinLength(DescriptionMin)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateOfEvent { get; set; }

        [Required]
        [StringLength(LocationMax), MinLength(LocationMin)]
        public string Location { get; set; } = null!;
    }
}
