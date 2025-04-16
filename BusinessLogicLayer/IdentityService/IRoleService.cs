using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.IdentityService
{
    public interface IRoleService
    {
        Task<List<RoleResponses>> GetAllAsync();
    }
}
