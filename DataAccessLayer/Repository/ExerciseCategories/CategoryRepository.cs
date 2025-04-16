using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.ExerciseCategories
{
    public class CategoryRepository : GenericRepository<ExerciseCategory>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ExerciseCategory> _dbSet;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {  
            _dbContext = dbContext;
            _dbSet = dbContext.Set<ExerciseCategory>();
        }

        public async Task<List<ExerciseCategory>> GetAllNoTrackingAsync()
        {
            return await QueryAllNoTracking()
                .ToListAsync();
        }
    }
}
