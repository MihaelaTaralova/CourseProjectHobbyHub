using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHubSystem.Web.ViewModels.Member.ServiceModel
{
    public class AllMembersFilteredAndPagedServiceModel
    {
        public AllMembersFilteredAndPagedServiceModel()
        {
            this.AllMembers = new List<AllMembersViewModel>();
        }
        public int TotalMembersCount { get; set; }

        public List<AllMembersViewModel> AllMembers { get; set; } 
    }
}
