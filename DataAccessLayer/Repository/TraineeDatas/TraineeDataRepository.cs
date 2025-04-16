using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.TraineeDatas
{
    public class TraineeDataRepository : GenericRepository<TraineeData>, ITraineeProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TraineeData> _dbSet;
        public TraineeDataRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TraineeData>();
        }

        public override async Task<List<TraineeData>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection)
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

        public override async Task<TraineeData> FindNoTrackingAsync(int traineeId)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TraineeId == traineeId);
        }

        public async Task<TraineeData> FindByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
