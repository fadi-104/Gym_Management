using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.TrainerProfiles
{
    public class TrainerProfileRepository : GenericRepository<TrainerProfile>, ITrainerProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TrainerProfile> _dbSet;
        public TrainerProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TrainerProfile>();
        }

        public override async Task<List<TrainerProfile>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .OrderBy(orderBy)
                .Include(x => x.User)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<TrainerProfile> FindNoTrackingAsync(int trainerId)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TrainerId == trainerId);
        }

        public async Task<TrainerProfile> FindByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
