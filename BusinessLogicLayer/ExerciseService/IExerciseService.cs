using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.ExerciseService
{
    public interface IExerciseService
    {
        Task CreateAsync(ExerciseRequests request);
        Task DeleteAsync(int id);
        Task<PagedResponse<List<ExerciseResponses>>> GetAllAsync(TableOptions options);
        Task<List<ExerciseResponses>> GetAllByCategoryAsync(string categoryName);
        Task<ExerciseResponses> GetAsync(int id);
        Task UpdateAsync(ExerciseRequests request);
    }
}
