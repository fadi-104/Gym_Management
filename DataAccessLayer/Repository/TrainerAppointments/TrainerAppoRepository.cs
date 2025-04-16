using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.TrainerAppointments
{
    public class TrainerAppoRepository : GenericRepository<TrainerAppointment>, ITrainerAppoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TrainerAppointment> _dbSet;
        public TrainerAppoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TrainerAppointment>();
        }

        public async Task<List<TrainerAppointment>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int trainerId)
        {
            if (orderDirection == "desc")
                orderBy = $"{orderBy} desc";

            return await QueryAllNoTracking()
                .Include(x => x.User)
                .Where(x => x.TrainerId == trainerId)
                .OrderBy(orderBy)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();   
        }

        public override async Task<TrainerAppointment> FindNoTrackingAsync(int id)
        {
            var foundEntity = await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return foundEntity;
        }

    }
}
