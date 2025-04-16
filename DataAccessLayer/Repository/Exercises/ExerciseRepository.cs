using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DataAccessLayer.Repository.Exercises
{
    public class ExerciseRepository : GenericRepository<Exercise>, IExercisRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Exercise> _dbSet;
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
            _dbSet = context.Set<Exercise>();
        }

        public override async Task<List<Exercise>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .OrderBy(orderBy)
                .Include(x => x.Category)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetAllByCategoryAsync(string categoryName)
        {
            return await QueryAllNoTracking()
                .Include(x => x.Category)
                .Where(x => x.Category.Name == categoryName)
                .ToListAsync();
        }

        public override async Task<Exercise> FindNoTrackingAsync(int id)
        {
            var foundEntity = await _dbSet.AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return foundEntity;
        }
    }
}
