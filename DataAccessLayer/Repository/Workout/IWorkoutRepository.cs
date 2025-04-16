using DataAccessLayer.Base;
using DomainEntitiesLayer.Entities;

namespace DataAccessLayer.Repository.Workout
{
    public interface IWorkoutRepository : IRepository<WorkoutData>
    {
        Task<WorkoutData> FindNoTrackingAsync(int id);
        Task<List<WorkoutData>> GetAllNoTrackingAsync(int skip, int pageSize, string orderBy, string orderDirection, int userId);
        Task<List<WorkoutData>> GetProgessNoTrackingAsync(string exerciseName, int userId);
    }
}
