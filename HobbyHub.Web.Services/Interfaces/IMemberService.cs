using HobbyBubSystem.Data.Models;
using HobbyBubSystem.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<HobbyUser>> GetHubMembers(int hubId);

    }
}
