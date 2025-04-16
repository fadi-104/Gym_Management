using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Base
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection);
        Task<TEntity> FindAsync(int id);
        Task<TEntity> FindNoTrackingAsync(int id);
        Task<int> CountAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
