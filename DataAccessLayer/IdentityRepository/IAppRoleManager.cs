using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.IdentityRepository
{
    public interface IAppRoleManager
    {
        Task<IdentityResult> CreateAsync(AppRole role);
        Task<IdentityResult> DeleteAsync(AppRole role);
        Task<IdentityResult> UpdateAsync(AppRole role);

        Task<List<AppRole>> GetAllAsync();
        Task<bool> RoleExistsAsync(string roleName);
    }
}
