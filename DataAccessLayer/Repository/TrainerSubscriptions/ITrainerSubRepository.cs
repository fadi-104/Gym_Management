using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.TrainerSubscriptions
{
    public interface ITrainerSubRepository : IRepository<TrainerSubscription>
    {
        Task<TrainerSubscription> FindNoTrackingAsync(int id);
        Task<TrainerSubscription> FindTrainerAsync(int traineeId);
        Task<List<TrainerSubscription>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int trainerId);
    }
}
