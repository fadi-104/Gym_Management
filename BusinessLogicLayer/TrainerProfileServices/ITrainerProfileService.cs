using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.TrainerProfileServices
{
    public interface ITrainerProfileService
    {
        Task CreateAsync(TrainerProfileRequests requests);
        Task<TrainerProfileResponses> GetAsync(int trainerId);
        Task UpdateAsync(TrainerProfileRequests requests);
    }
}
