using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.SubscriptionPlans
{
    public interface ISubscriptionPlanRepository : IRepository<SubscriptionPlan>
    {
        Task<List<SubscriptionPlan>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, bool? isActive);
    }
}
