using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Constants;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.SubscriptionPlans;
using DataAccessLayer.Repository.Subscriptions;
using DataAccessLayer.Repository.TraineeDatas;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.SubscriptionServices
{
    public class SubscriptionService : ISubscriptionServices
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IAppUserManager appUserManager, IMapper mapper, ISubscriptionPlanRepository subscriptionPlanRepository)
        {
             _subscriptionRepository = subscriptionRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<SubscriptionResponses>>> GetAllAsync(TableOptions options)
        {
            var totalCount = await _subscriptionRepository.CountAsync();

            var list = await _subscriptionRepository.GetAllNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection);
            var responses = list.Select(x => _mapper.Map<SubscriptionResponses>(x)).ToList();

            return new PagedResponse<List<SubscriptionResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<SubscriptionResponses> GetAsync(int id)
        {
            var entity = await _subscriptionRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<SubscriptionResponses>(entity);
            return response;
        }

        public async Task CreateAsync(SubscriptionRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _appUserManager.CheckRoleAsync(requests.UserId, UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can Choose Plan.");

            var entity = _mapper.Map<Subscription>(requests);

            var plan = await _subscriptionPlanRepository.FindNoTrackingAsync(entity.SubscriptionId.Value);

            var term = plan.Period;

            if (plan.TimeUnit.ToLower().Contains("month"))
            {
                entity.StartDate = DateTime.Now;
                entity.EndDate = entity.StartDate.AddMonths(term);
            }
            else if (plan.TimeUnit.ToLower().Contains("year"))
            {
                entity.StartDate = DateTime.Now;
                entity.EndDate = entity.StartDate.AddYears(term);
            }

            await _subscriptionRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _subscriptionRepository.FindAsync(id);
            if (entity is null)
                throw new DataValidationException("Id must no to be set");

            await _subscriptionRepository.DeleteAsync(entity);
        }

    }
}
