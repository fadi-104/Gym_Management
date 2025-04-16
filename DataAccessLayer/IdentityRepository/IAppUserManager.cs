using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer.IdentityRepository
{
    public interface IAppUserManager
    {
        Task<int> CountAsync();
        Task<List<AppUser>> GetAllAsNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, string role, bool? isActive);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> UpdateAsync(AppUser user);
        Task<IdentityResult> DeleteAsync(AppUser user);

        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string role);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<AppUser> FindByIdAsync(string id);
        Task<AppUser> FindByNameAsync(string userName);
        Task<bool> IsInRoleAsync(AppUser user, string role);
        Task<AppUser> GetByIdAsync(int id);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> CheckRoleAsync(int id, int roleId);
    }
}
