using AutoMapper;
using BusinessLogicLayer.Constants;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TrainerSubscriptions;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.TrainerSubServices
{
    public class TrainerSubService : ITrainerSubService
    {
        private readonly ITrainerSubRepository _trainerSubRepository;
        private readonly IAppUserManager _userManager;
        private readonly IMapper _mapper;

        public TrainerSubService(ITrainerSubRepository trainerSubRepository, IAppUserManager appUserManager, IMapper mapper)
        {
            _trainerSubRepository = trainerSubRepository;
            _userManager = appUserManager;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<TrainerSubscriptionResponses>>> GetAllAsync(TableOptions options, int trainrtId)
        {
            var totalCount = await _trainerSubRepository.CountAsync();

            var list = await _trainerSubRepository.GetAllNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection, trainrtId);
            var responses = list.Select(x => _mapper.Map<TrainerSubscriptionResponses>(x)).ToList();

            return new PagedResponse<List<TrainerSubscriptionResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<TrainerSubscriptionResponses> GetAsync(int id)
        {
            var entity = await _trainerSubRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<TrainerSubscriptionResponses>(entity);
            return response;
        }

        public async Task<TrainerSubscriptionResponses> GetTrainerAsync(int traineeId)
        {
            var entity = await _trainerSubRepository.FindTrainerAsync(traineeId);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<TrainerSubscriptionResponses>(entity);
            return response;
        }

        public async Task CreateAsync(TrainerSubscriptionRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _userManager.CheckRoleAsync(requests.TrainerId, UserRoleInt.Trainer) && !await _userManager.CheckRoleAsync(requests.TraineeId, UserRoleInt.Trainee))
                throw new NoPermissionException("The procedure cannot be completed.");

            var entity = _mapper.Map<TrainerSubscription>(requests);
            await _trainerSubRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _trainerSubRepository.FindAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _trainerSubRepository.DeleteAsync(entity);
        }
    }
}
