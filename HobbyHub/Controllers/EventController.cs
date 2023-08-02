using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            this.eventService = _eventService;
        }

        public async Task<IActionResult> All(int hubId)
        {
            var allEvents = await eventService.GetAllEventsAsync();
            var activeEvents = allEvents.Where(e => e.IsActive == true).ToList();

            var eventViewModel = activeEvents.Select(e => new EventIntroViewModel
            {
                Id = e.Id,
                Title = e.Title,
                DateOfEvent = e.DateOfEvent,
                Location = e.Location,
                HubId = e.HubId,
                IsActive = e.IsActive
            }).ToList();

            var viewModel = new AllEventsViewModel
            {
                Events = eventViewModel,
                HubId = hubId
            };

            return View(viewModel);
        }
    }
}
