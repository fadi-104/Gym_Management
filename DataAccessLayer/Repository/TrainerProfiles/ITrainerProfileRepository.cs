using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.TrainerProfiles
{
    public interface ITrainerProfileRepository : IRepository<TrainerProfile>
    {
        Task<TrainerProfile> FindByIdAsync(int id);
        Task<TrainerProfile> FindNoTrackingAsync(int trainerId);
        Task<List<TrainerProfile>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection);
    }
}
