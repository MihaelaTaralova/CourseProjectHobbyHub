using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Article;
using HobbyHubSystem.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Web.Services.Services
{
    public class EventService : IEventService
    {
        private readonly HobbyHubDbContext dbContext;

        public EventService(HobbyHubDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddEventAsync(AddEventViewModel eventViewModel, Guid CreatorId)
        {
            var addEvent = new Event()
            {
                Title = eventViewModel.Title,
                Description = eventViewModel.Description,
                CreatorId = CreatorId,
                Location = eventViewModel.Location,
                DateOfEvent = eventViewModel.DateOfEvent,
                HubId = eventViewModel.HubId
            };

            await dbContext.Events.AddAsync(addEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEvent(int Id)
        {
            var eventForDelete = await dbContext.Events.FindAsync(Id);

            if (eventForDelete == null)
            {
                throw new ArgumentException("Event not found");
            }

            eventForDelete.IsActive = false;
            dbContext.Events.Update(eventForDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditEvent(int Id, EditEventViewModel model)
        {
            var editEvent = await dbContext.Events.FindAsync(Id);

            if (editEvent == null)
            {
                throw new ArgumentException("Event not found");
            }

            editEvent.Title = model.Title;
            editEvent.Description = model.Description;
            editEvent.Location = model.Location;
            editEvent.DateOfEvent = model.DateOfEvent;

            dbContext.Events.Update(editEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEventsAsync(int hubId)
        {
            return await dbContext.Events.Where(a => a.HubId == hubId && a.IsActive == true).ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int Id)
        {
            return await dbContext.Events.FindAsync(Id);
        }

        public async Task<Event> GetEventByNameAsync(string title)
        {
            return await dbContext.Events.FirstOrDefaultAsync(e => e.Title == title);
        }

        public async Task<bool> IsUserJoinedEvent(int Id, Guid userId)
        {
            var isUserJoined = await dbContext.HobbyUserEvents.FirstOrDefaultAsync(e => e.EventId == Id && e.HobbyUserId == userId);

            return isUserJoined != null;
        }

        public async Task JoinEventAsync(int Id, Guid userId)
        {
            var eventToJoin = await dbContext.Events.FindAsync(Id);

            if (eventToJoin == null)
            {
                throw new ArgumentException("Event not found");
            }

            var isUserJoined = await dbContext.HobbyUserEvents.FirstOrDefaultAsync(e => e.EventId == Id && e.HobbyUserId == userId);

            if (isUserJoined != null)
            {
                throw new ArgumentException("You already joined the event.");
            }

            var eventParticipation = new HobbyUserEvent
            {
                EventId = Id,
                HobbyUserId = userId
            };

            dbContext.HobbyUserEvents.Add(eventParticipation);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EventViewModel> GetEventAsync(int id)
        {
            var currentEvent = await dbContext.Events
                .FirstOrDefaultAsync(a => a.Id == id);

            if (currentEvent == null)
            {
                throw new ArgumentException("Event not found");
            }

            var viewModel = new EventViewModel
            {
                Id = currentEvent.Id,
                Title = currentEvent.Title,
                Description = currentEvent.Description,
                DateOfEvent = currentEvent.DateOfEvent,
                Location = currentEvent.Location,
                HubId = currentEvent.HubId,

            };

            return viewModel;
        }

        public EventViewModel ConvertEventModelToViewModel(Event eventModel)
        {
            return new EventViewModel
            {
                Id = eventModel.Id,
                Title = eventModel.Title,
                Description = eventModel.Description,
                DateOfEvent = eventModel.DateOfEvent,
                Location = eventModel.Location,
                HubId = eventModel.HubId,
            };
        }

    }
}
