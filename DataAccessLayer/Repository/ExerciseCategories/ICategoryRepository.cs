using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.ExerciseCategories
{
    public interface ICategoryRepository : IRepository<ExerciseCategory>
    {
        Task<List<ExerciseCategory>> GetAllNoTrackingAsync();
    }
}
