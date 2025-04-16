using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Base
{
    public abstract class GenericRepository<TEntity> where  TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();           
        }

        public virtual IQueryable<TEntity> QueryAllNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<List<TEntity>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection)
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            var foundEntity = await _dbSet.FindAsync(id);
            return foundEntity;
        }

        public virtual async Task<TEntity> FindNoTrackingAsync(int id)
        {
            var foundEntity = await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return foundEntity;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
             _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var newEntity = _dbSet.Attach(entity);
            newEntity.State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
