using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.TrainerSubServices
{
    public interface ITrainerSubService
    {
        Task CreateAsync(TrainerSubscriptionRequests requests);
        Task DeleteAsync(int id);
        Task<PagedResponse<List<TrainerSubscriptionResponses>>> GetAllAsync(TableOptions options, int trainrtId);
        Task<TrainerSubscriptionResponses> GetAsync(int id);
        Task<TrainerSubscriptionResponses> GetTrainerAsync(int traineeId);
    }
}
