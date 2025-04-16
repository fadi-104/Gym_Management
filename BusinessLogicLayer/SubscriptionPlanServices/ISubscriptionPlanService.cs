using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.SubscriptionPlanServices
{
    public interface ISubscriptionPlanService
    {
        Task CreateAsync(SubscriptionPlanRequests requests);
        Task DeleteAsync(int Id);
        Task<PagedResponse<List<SubscriptionPlanResponses>>> GetAllAsync(TableOptions options, bool? isActive);
        Task<SubscriptionPlanResponses> GetAsync(int id);
        Task UpdateAsync(SubscriptionPlanRequests requests);
    }
}
