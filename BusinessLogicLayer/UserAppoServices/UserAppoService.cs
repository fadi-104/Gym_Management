using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Constants;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TraineeDatas;
using DataAccessLayer.Repository.UserAppo;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;

namespace BusinessLogicLayer.UserAppoServices
{
    public class UserAppoService : IUserAppoService
    {
        private readonly IUserAppoRepository _userAppoRepository;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;
        public UserAppoService(IUserAppoRepository userAppoRepository, IAppUserManager appUserManager, IMapper mapper)
        {
            _userAppoRepository = userAppoRepository;
            _appUserManager = appUserManager;
            _mapper = mapper;
        }

        public async Task<List<UserAppointmentResponses>> GetAllForTraineeAsync(int traineeId, DateTime date)
        {
            var list = await _userAppoRepository.GetAllForTraineeAsync(traineeId, date);
            var responses = list.Select(x => _mapper.Map<UserAppointmentResponses>(x)).ToList();

            return responses;
        }

        public async Task<List<UserAppointmentResponses>> GetAllForTrainerAsync(int trainerId, DateTime date)
        {
            var list = await _userAppoRepository.GetAllForTrainerAsync(trainerId, date);
            var responses = list.Select(x => _mapper.Map<UserAppointmentResponses>(x)).ToList();

            return responses;
        }

        public async Task<UserAppointmentResponses> GetAsync(int id)
        {
            var entity = await _userAppoRepository.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<UserAppointmentResponses>(entity);
            return response;
        }

        public async Task CreateAsync(UserAppointmentRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if (!await _appUserManager.CheckRoleAsync(requests.UserId, UserRoleInt.Trainee))
                throw new NoPermissionException("Only users with Trainee role can Choose Date.");

            var entity = _mapper.Map<UserAppointment>(requests);
            await _userAppoRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _userAppoRepository.FindAsync(id);
            if (entity is null)
                throw new DataValidationException("Id must no to be set");

            await _userAppoRepository.DeleteAsync(entity);
        }
    }
}
