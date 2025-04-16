using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.IdentityRepository
{
    public class AppRoleManager : RoleManager<AppRole>, IAppRoleManager
    {
        private readonly ApplicationDbContext _dbContext;
        public AppRoleManager(IRoleStore<AppRole> store,
            IEnumerable<IRoleValidator<AppRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<AppRole>> logger,
            ApplicationDbContext dbContext) : base(store, roleValidators, keyNormalizer, errors, logger)
        {  
            _dbContext = dbContext;
        }

        public async Task<List<AppRole>> GetAllAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }


    }
}
