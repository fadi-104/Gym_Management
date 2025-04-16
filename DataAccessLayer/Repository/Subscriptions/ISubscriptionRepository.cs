using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.Subscriptions
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<Subscription> FindNoTrackingAsync(int id);
        Task<List<Subscription>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection);
    }
}
