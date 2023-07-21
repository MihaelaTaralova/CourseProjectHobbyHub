using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHub.Web.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task CreateRoles();
        Task AddUsersToRoles();
    }
}
