using AutoMapper;
using Core.Models;
using DataAccessLayer.Repository.SubscriptionPlans;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.SubscriptionPlanServices
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IMapper _mapper;
        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository, IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<SubscriptionPlanResponses>>> GetAllAsync(TableOptions options, bool? isActive)
        {
            var totalCount = await _subscriptionPlanRepository.CountAsync();

            var list = await _subscriptionPlanRepository.GetAllNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection, isActive);
            var responses = list.Select(x => _mapper.Map<SubscriptionPlanResponses>(x)).ToList();

            return new PagedResponse<List<SubscriptionPlanResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<SubscriptionPlanResponses> GetAsync(int id)
        {
            var entity = await _subscriptionPlanRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<SubscriptionPlanResponses>(entity);
            return response;
        }

        public async Task CreateAsync(SubscriptionPlanRequests requests)
        {
            if (requests.Id >  0)
                throw new DataValidationException("Id must no to be set");

            var entity = _mapper.Map<SubscriptionPlan>(requests);
            await _subscriptionPlanRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(SubscriptionPlanRequests requests)
        {
            if (!requests.Id.HasValue)
                throw new DataValidationException("Id must be provided");

            var entity = await _subscriptionPlanRepository.FindNoTrackingAsync(requests.Id.Value);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            entity = _mapper.Map<SubscriptionPlan>(requests);
            await _subscriptionPlanRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await _subscriptionPlanRepository.FindAsync(Id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _subscriptionPlanRepository.DeleteAsync(entity);
        }
    }
}
