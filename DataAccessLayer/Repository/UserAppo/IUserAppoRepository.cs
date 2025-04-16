using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.UserAppo
{
    public interface IUserAppoRepository : IRepository<UserAppointment>
    {
        Task<UserAppointment> FindNoTrackingAsync(int id);
        Task<List<UserAppointment>> GetAllForTraineeAsync(int traineeId, DateTime date);
        Task<List<UserAppointment>> GetAllForTrainerAsync(int trainerId, DateTime date);
    }
}
