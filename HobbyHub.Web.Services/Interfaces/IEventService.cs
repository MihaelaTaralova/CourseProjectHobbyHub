
using HobbyBubSystem.Data.Models;
using HobbyHubSystem.Web.ViewModels.Event;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync(int hubId);

        Task<Event> GetEventByIdAsync(int Id);

        Task<EventViewModel> GetEventAsync(int id);

        Task AddEventAsync(AddEventViewModel eventViewModel, Guid CreatorId);

        Task EditEvent(int Id, EditEventViewModel model);

        Task DeleteEvent(int Id);

        Task<Event> GetEventByNameAsync(string title);

        Task JoinEventAsync(int eventId, Guid userId);

        Task<bool> IsUserJoinedEvent(int eventId, Guid userId);

        EventViewModel ConvertEventModelToViewModel(Event eventModel);

    }
}
