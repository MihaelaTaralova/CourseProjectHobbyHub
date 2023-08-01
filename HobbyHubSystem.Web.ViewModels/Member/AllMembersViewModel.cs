namespace HobbyHubSystem.Web.ViewModels.Member
{
    public class AllMembersViewModel
    {
        public AllMembersViewModel()
        {
            this.Members = new List<MemberViewModel>();
        }
        public int HubId { get; set; }
        public List<MemberViewModel> Members { get; set; }
    }
}
