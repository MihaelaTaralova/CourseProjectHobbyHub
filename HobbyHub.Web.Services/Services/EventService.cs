using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Event;

namespace HobbyHub.Web.Services.Services
{
    public class EventService : IEventService
    {
        private readonly HobbyHubDbContext dbContext;

        public EventService(HobbyHubDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public Task AddEventAsync(AddEventViewModel eventViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEvent(int Id)
        {
            throw new NotImplementedException();
        }

        public Task EditEvent(int Id, EditEventViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByNameAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserJoinedEvent(int eventId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task JoinEventAsync(int eventId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
