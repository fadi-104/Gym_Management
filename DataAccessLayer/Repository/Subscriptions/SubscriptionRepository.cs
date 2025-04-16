using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Subscriptions
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<Subscription>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .OrderBy(orderBy)
                .Include(x => x.User)
                .Include(x => x.Plan)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<Subscription> FindNoTrackingAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Plan)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
