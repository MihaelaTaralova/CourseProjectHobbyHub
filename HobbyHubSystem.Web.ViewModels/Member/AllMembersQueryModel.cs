
using HobbyHubSystem.Web.ViewModels.Member.Enum;
using System.ComponentModel.DataAnnotations;

using static HobbyHubSystem.Common.GeneralConstants;

namespace HobbyHubSystem.Web.ViewModels.Member
{
    public class AllMembersQueryModel
    {
        public AllMembersQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.MembersPerPage = EntitiesPerPage;
            this.Members = new List<AllMembersViewModel>();
        }
        [Display(Name = "Search by word")]
        public string SearchString { get; set; }

        [Display(Name = "Sort by")]
        public MemberSorting MemberSorting { get; set; }

        public int CurrentPage { get; set; }

        public int MembersPerPage { get; set; }

        public int MemberCount { get; set; }

        public List<AllMembersViewModel> Members { get; set; }
    }
}
