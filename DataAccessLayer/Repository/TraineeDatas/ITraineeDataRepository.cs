using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.TraineeDatas
{
    public interface ITraineeProfileRepository : IRepository<TraineeData>
    {
        Task<TraineeData> FindByIdAsync(int id);
        Task<TraineeData> FindNoTrackingAsync(int trainerId);
        Task<List<TraineeData>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection);
    }
}
