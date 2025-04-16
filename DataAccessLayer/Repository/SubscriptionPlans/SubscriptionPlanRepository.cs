using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.SubscriptionPlans
{
    public class SubscriptionPlanRepository : GenericRepository<SubscriptionPlan>, ISubscriptionPlanRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<SubscriptionPlan> _dbSet;
        public SubscriptionPlanRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<SubscriptionPlan>();

        }

        public async Task<List<SubscriptionPlan>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, bool? isActive)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            var query = QueryAllNoTracking();

            if (isActive is null)
            {
                return await query
                    .OrderBy(orderBy)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _dbSet.AsNoTracking()
                .Where(x => x.IsActive == isActive)
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
