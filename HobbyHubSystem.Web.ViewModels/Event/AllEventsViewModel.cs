namespace HobbyHubSystem.Web.ViewModels.Event
{
    public class AllEventsViewModel
    {
        public AllEventsViewModel()
        {
            this.Events = new List<EventIntroViewModel>();
        }
        public int HubId { get; set; }

        public List<EventIntroViewModel> Events { get; set; }
    }
}
