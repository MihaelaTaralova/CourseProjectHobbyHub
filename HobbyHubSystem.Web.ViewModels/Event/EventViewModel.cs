namespace HobbyHubSystem.Web.ViewModels.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string Location { get; set; }

        public int HubId { get; set; }
    }
}
