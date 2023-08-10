using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

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
            if (hubId <= 0)
            {
                return BadRequest();
            }

            var allEvents = await eventService.GetAllEventsAsync(hubId);
            //var activeEvents = allEvents.Where(e => e.IsActive == true).ToList();

            var eventViewModel = allEvents.Select(e => new EventIntroViewModel
            {
                Id = e.Id,
                Title = e.Title,
                DateOfEvent = e.DateOfEvent,
                Location = e.Location,
                HubId = e.HubId
            }).ToList();

            var viewModel = new AllEventsViewModel
            {
                Events = eventViewModel,
                HubId = hubId
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventViewModel = await eventService.GetEventAsync(id);

            if (eventViewModel == null)
            {
                return NotFound();
            }
            return View(eventViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult AddEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            AddEventViewModel model = new()
            {
                HubId = id

            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventViewModel eventViewModel)
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                await eventService.AddEventAsync(eventViewModel, new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                var addedEvent = await eventService.GetEventByNameAsync(eventViewModel.Title);
                var addedEventViewModel = eventService.ConvertEventModelToViewModel(addedEvent); 
                return View("ViewAddedEvent", addedEventViewModel);
            }

            return View(eventViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var currentEvent = await eventService.GetEventByIdAsync(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            if (currentEvent.IsActive == false)
            {
                return NotFound();
            }

            var eventModel = new DeleteEventViewModel
            {
                Id = currentEvent.Id,
                Title = currentEvent.Title,
                HubId = currentEvent.HubId,
            };

            return View(eventModel);
        }

        [HttpPost, ActionName("DeleteEvent")]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> DeleteEventConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await eventService.DeleteEvent(id);
                return RedirectToAction("All", "Event");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> EditEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var currentEvent = await eventService.GetEventByIdAsync(id);
            if (currentEvent == null)
            {
                return NotFound();
            }

            if (currentEvent.IsActive == false)
            {
                return NotFound();
            }

            var eventViewModel = new EditEventViewModel
            {
                Id = currentEvent.Id,
                Title = currentEvent.Title,
                Description = currentEvent.Description,
                DateOfEvent = currentEvent.DateOfEvent,
                Location = currentEvent.Location
            };

            return View(eventViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(int id, EditEventViewModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await eventService.EditEvent(id, model);
                    return RedirectToAction("ViewEvent", "Event", new { id = model.Id, name = model.Title }, fragment: null);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);

        }

    }
}
