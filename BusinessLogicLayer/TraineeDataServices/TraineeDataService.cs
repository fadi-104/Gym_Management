using AutoMapper;
using BusinessLogicLayer.Constants;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TraineeDatas;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;


namespace BusinessLogicLayer.TraineeDataServices
{
    public class TraineeDataService : ITraineeDataService
    {
        private readonly ITraineeProfileRepository _traineeDataRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserManager _appUserManager;
        public TraineeDataService(ITraineeProfileRepository traineeDataRepository, IMapper mapper, IAppUserManager appUserManager)
        {
            _traineeDataRepository = traineeDataRepository;
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        public async Task<TraineeDataResponses> GetAsync(int traineeId)
        {
            var entity = await _traineeDataRepository.FindNoTrackingAsync(traineeId);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");
            
            var response = _mapper.Map<TraineeDataResponses>(entity);
            return response;
        }

        public async Task CreateAsync(TraineeDataRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _appUserManager.CheckRoleAsync(requests.TraineeId, UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can create Trainee Data.");

            var entity = _mapper.Map<TraineeData>(requests);
            await _traineeDataRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(TraineeDataRequests requests)
        {
            if (!requests.Id.HasValue)
                throw new DataValidationException("Id must be set");

            var entity = await _traineeDataRepository.FindByIdAsync(requests.Id.Value);
            if (entity is null)
                throw new DataValidationException("The provided entity is not found");

            if (!await _appUserManager.CheckRoleAsync(requests.TraineeId, UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can Update Trainee Data.");

            entity = _mapper.Map<TraineeData>(requests);
            await _traineeDataRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _traineeDataRepository.FindAsync(id);
            if (entity is null)
                throw new DataValidationException("Id must no to be set");

            await _traineeDataRepository.DeleteAsync(entity);
        }

    }
}
