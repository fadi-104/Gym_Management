using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.WorkoutServices
{
    public interface IWorkoutService
    {
        Task CreateAsync(WorkoutDataRequests requests);
        Task DeleteAsync(int id);
        Task<PagedResponse<List<WorkoutDataResponses>>> GetAllAsync(TableOptions options, int userId);
        Task<WorkoutDataResponses> GetAsync(int id);
        Task<List<ProgessResponses>> GetProgressAsync(string exerciseName, int userId);
        Task UpdateAsync(WorkoutDataRequests requests);
    }
}
