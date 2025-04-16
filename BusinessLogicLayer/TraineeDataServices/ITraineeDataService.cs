using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.TraineeDataServices
{
    public interface ITraineeDataService
    {
        Task CreateAsync(TraineeDataRequests requests);
        Task DeleteAsync(int id);
        Task<TraineeDataResponses> GetAsync(int trainerId);
        Task UpdateAsync(TraineeDataRequests requests);
    }
}
