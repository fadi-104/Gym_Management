using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.Exercises
{
    public interface IExercisRepository : IRepository<Exercise>
    {
        Task<Exercise> FindNoTrackingAsync(int id);
        Task<List<Exercise>> GetAllByCategoryAsync(string categoryName);
        Task<List<Exercise>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection);
    }
}
