using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.SubscriptionServices
{
    public interface ISubscriptionServices
    {
        Task CreateAsync(SubscriptionRequests requests);
        Task DeleteAsync(int id);
        Task<PagedResponse<List<SubscriptionResponses>>> GetAllAsync(TableOptions options);
        Task<SubscriptionResponses> GetAsync(int id);
    }
}
