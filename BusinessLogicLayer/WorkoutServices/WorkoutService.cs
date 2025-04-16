using AutoMapper;
using BusinessLogicLayer.Constants;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.Workout;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.WorkoutServices
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserManager _appUserManager;
        public WorkoutService(IWorkoutRepository workoutRepository, IAppUserManager appUserManager, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
            _appUserManager = appUserManager;

        }

        public async Task<PagedResponse<List<WorkoutDataResponses>>> GetAllAsync(TableOptions options, int userId)
        {
            var totalCount = await _workoutRepository.CountAsync();

            var list = await _workoutRepository.GetAllNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection, userId);
            var responses = list.Select(x => _mapper.Map<WorkoutDataResponses>(x)).ToList();

            return new PagedResponse<List<WorkoutDataResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<List<ProgessResponses>> GetProgressAsync(string exerciseName, int userId)
        {
            var list = await _workoutRepository.GetProgessNoTrackingAsync(exerciseName, userId);
            var responses = list.Select(x => new ProgessResponses() { Performance =  x.RepsNumber * x.Weight, Date = x.Date }).ToList();
            
            return responses;
        }

        public async Task<WorkoutDataResponses> GetAsync(int id)
        {
            var entity = _workoutRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<WorkoutDataResponses>(entity);
            return response;
        }

        public async Task CreateAsync(WorkoutDataRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _appUserManager.CheckRoleAsync(requests.UserId,UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can create Workout.");

            var entity = _mapper.Map<WorkoutData>(requests);
            await _workoutRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(WorkoutDataRequests requests)
        {
            if (!requests.Id.HasValue)
                throw new DataValidationException("Id must be set");

            var entity = await _workoutRepository.FindNoTrackingAsync(requests.Id.Value);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            if (!await _appUserManager.CheckRoleAsync(requests.UserId, UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can Update Workout.");

            entity = _mapper.Map<WorkoutData>(requests);
            await _workoutRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _workoutRepository.FindAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _workoutRepository.DeleteAsync(entity);
        }
    }
}
