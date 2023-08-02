
using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Event;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();

        Task<Event> GetEventByIdAsync(int Id);

        Task AddEventAsync(AddEventViewModel eventViewModel);

        Task EditEvent(int Id, EditEventViewModel model);

        Task DeleteEvent(int Id);

        Task<Event> GetEventByNameAsync(string title);

        Task JoinEventAsync(int eventId, Guid userId);

        Task<bool> IsUserJoinedEvent(int eventId, Guid userId);

    }
}
