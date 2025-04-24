using AutoMapper;
using BusinessLogicLayer.Constants;
using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.Storage;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TrainerProfiles;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.TrainerProfileServices
{
    public class TrainerProfileService : ITrainerProfileService
    {
        private readonly ITrainerProfileRepository _trainerProfileRepository;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public TrainerProfileService(ITrainerProfileRepository trainerProfileRepository,
            IAppUserManager appUserManager,
            IMapper mapper,
            IStorageService storageService)
        {
            _trainerProfileRepository = trainerProfileRepository;
            _appUserManager = appUserManager;
            _mapper = mapper;
            _storageService = storageService;
        }
        public async Task<TrainerProfileResponses> GetAsync(int trainerId)
        {
            var entity = await _trainerProfileRepository.FindNoTrackingAsync(trainerId);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<TrainerProfileResponses>(entity);

            return response;
        }

        public async Task CreateAsync(TrainerProfileRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _appUserManager.CheckRoleAsync(requests.TrainerId.Value, UserRoleInt.Trainer))
                throw new NoPermissionException("Only users with Trainer role can create Profile.");

            var entity = _mapper.Map<TrainerProfile>(requests);
            await _trainerProfileRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(TrainerProfileRequests requests)
        {
            using(var transaction = await _appUserManager.BeginTransactionAsync())
            {
                if (!requests.Id.HasValue)
                    throw new DataValidationException("Id must be set");

                var entityProfile = await _trainerProfileRepository.FindByIdAsync(requests.Id.Value);
                if (entityProfile is null)
                    throw new DataValidationException("The provided entity is not found");

                if (!await _appUserManager.CheckRoleAsync(requests.TrainerId.Value, UserRoleInt.Trainer))
                    throw new NoPermissionException("Only users with Trainer role can Update Profile.");

                entityProfile = requests.ToTrainerProfile(entityProfile);
                var entityTrainer = requests.ToAppUserProfile(entityProfile.User);
                entityTrainer.Image = await _storageService.ReplaceFileAsync(requests.ImageFile, "Gym_Management\\Gym_Management\\Images\\Users", entityTrainer.Image);

                await _trainerProfileRepository.UpdateAsync(entityProfile);
                await _appUserManager.UpdateAsync(entityTrainer);

                await transaction.CommitAsync();
            }
        }
    }
}
