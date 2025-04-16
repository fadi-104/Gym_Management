using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.TrainerSubscriptions
{
    public class TrainerSubRepository : GenericRepository<TrainerSubscription>, ITrainerSubRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TrainerSubscription> _dbSet;
        public TrainerSubRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TrainerSubscription>();
        }

        public async Task<List<TrainerSubscription>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int trainerId)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .Include(x => x.Trainee)
                .Include(x => x.Trainer)
                .Where(x => x.TrainerId == trainerId)
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }
 

        public override async Task<TrainerSubscription> FindNoTrackingAsync(int id)
        {
            return await _dbSet.Include(x => x.Trainee)
                .Include(x => x.Trainer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TrainerSubscription> FindTrainerAsync(int traineeId)
        {
            return await _dbSet.Include(x => x.Trainee)
                .Include(x => x.Trainer)
                .FirstOrDefaultAsync(x => x.TraineeId == traineeId);

        }
    }
}
