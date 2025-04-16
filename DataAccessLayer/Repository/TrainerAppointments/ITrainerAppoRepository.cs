using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.TrainerAppointments
{
    public interface ITrainerAppoRepository : IRepository<TrainerAppointment>
    {
        Task<TrainerAppointment> FindNoTrackingAsync(int id);
        Task<List<TrainerAppointment>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int trainerId);
    }
}
