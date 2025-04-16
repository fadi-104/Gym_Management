using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.ExerciseCategoryService
{
    public interface ICategoryService
    {
        Task CreateAsync(ExerciseCategoryRequests request);
        Task DeleteAsync(int id);
        Task<List<ExerciseCategoryResponses>> GetAllAsync();
        Task<ExerciseCategoryResponses> GetAsync(int id);
        Task UpdateAsync(ExerciseCategoryRequests request);
    }
}
