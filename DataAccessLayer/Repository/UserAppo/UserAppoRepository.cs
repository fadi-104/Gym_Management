using System.Linq.Dynamic.Core;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.UserAppo
{
    public class UserAppoRepository : GenericRepository<UserAppointment>, IUserAppoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<UserAppointment> _dbSet;

        public UserAppoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
             _dbContext = dbContext;
             _dbSet = dbContext.Set<UserAppointment>();
        }

        public async Task<List<UserAppointment>> GetAllForTraineeAsync(int traineeId, DateTime date)
        {

            return await QueryAllNoTracking()
                .Include(x => x.User)
                .Include(x => x.TrainerAppointment)
                .Where(x => x.UserId == traineeId && x.TrainerAppointment.StartDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<List<UserAppointment>> GetAllForTrainerAsync(int trainerId, DateTime date)
        {

            return await QueryAllNoTracking()
                .Include(x => x.User)
                .Include(x => x.TrainerAppointment)
                .Where(x => x.TrainerAppointment.TrainerId == trainerId && x.TrainerAppointment.StartDate.Date == date.Date)
                .ToListAsync();
        }

        public override async Task<UserAppointment> FindNoTrackingAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.TrainerAppointment)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
