using System.Data;
using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Workout
{
    public class WorkoutRepository : GenericRepository<WorkoutData>, IWorkoutRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<WorkoutData> _dbSet;

        public WorkoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
             _dbContext = dbContext;
            _dbSet = dbContext.Set<WorkoutData>();
        }

        public async Task<List<WorkoutData>> GetProgessNoTrackingAsync(string exerciseName, int userId)
        {

            return await QueryAllNoTracking()
                .Include(x => x.User)
                .Include(x => x.Exercise)
                .Where(x => x.Exercise.Name == exerciseName && x.User.Id == userId )
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<WorkoutData>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int userId)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .Include(x => x.User)
                .Include(x => x.Exercise)
                .Where(x => x.User.Id == userId)
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public override async Task<WorkoutData> FindNoTrackingAsync(int id)
        {
            return await _dbSet.Include(x => x.User)
                .Include(x => x.Exercise)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
