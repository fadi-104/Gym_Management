
using DomainEntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Dynamic.Core;

namespace DataAccessLayer.IdentityRepository
{
    public class AppUserManager : UserManager<AppUser>, IAppUserManager
    {
        private readonly ApplicationDbContext _dbContext;
        public AppUserManager(IUserStore<AppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger,
            ApplicationDbContext dbContext) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {  
            _dbContext = dbContext;
           
        }

        public async Task<List<AppUser>> GetAllAsNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, string role, bool? isActive)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            if (isActive is null)
            {
                return await (from user in Users
                              join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                              join Role in _dbContext.Roles on userRole.RoleId equals Role.Id
                              where Role.Name == role
                              select user).AsNoTracking()
                              .OrderBy(orderBy)
                              .Skip(skip)
                              .Take(pageSize)
                              .ToListAsync();
            }

            return await (from user in Users
                          join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                          join Role in _dbContext.Roles on userRole.RoleId equals Role.Id
                          where Role.Name == role && user.IsActive == isActive
                          select user).AsNoTracking()
                          .OrderBy(orderBy)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Users.CountAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task<bool> CheckRoleAsync(int TrainerId, int roleId)
        {
            return await _dbContext.UserRoles
            .AnyAsync(x => x.UserId == TrainerId && x.RoleId == roleId);
        }
    }

    
}
