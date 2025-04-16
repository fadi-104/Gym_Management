using AutoMapper;
using BusinessLogicLayer.Constants;
using Core.Models;
using DataAccessLayer.IdentityRepository;
using DataAccessLayer.Repository.TrainerAppointments;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using ShopApp.Core.Exceptions;


namespace BusinessLogicLayer.TrainerAppointmentService
{
    public class TrainerAppoService : ITrainerAppoService
    {
        private readonly ITrainerAppoRepository _trainerAppo;
        private readonly IAppUserManager _userManager;
        private readonly IMapper _mapper;

        public TrainerAppoService(ITrainerAppoRepository trainerAppo, IAppUserManager userManager, IMapper mapper)
        {
            _trainerAppo = trainerAppo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<TrainerAppoResponses>>> GetAllAsync(TableOptions options, int trainerId)
        {
            
            var totalCount = await _trainerAppo.CountAsync();

            var list = await _trainerAppo.GetAllNoTrackingAsync(options.Skip, options.PageSize, options.OrderBy, options.OrderByDirection, trainerId);
            var responses = list.Select(x => _mapper.Map<TrainerAppoResponses>(x)).ToList();

            return new PagedResponse<List<TrainerAppoResponses>>
            {
                Data = responses,
                TotalCount = totalCount,
            };
        }

        public async Task<TrainerAppoResponses> GetAsync(int id)
        {
            var entity = await _trainerAppo.FindNoTrackingAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            var response = _mapper.Map<TrainerAppoResponses>(entity);

            return response;
        }

        public async Task CreateAsync(TrainerAppoRequests requests)
        {
            if (requests.Id > 0)
                throw new DataValidationException("Id must no to be set");

            if(!await _userManager.CheckRoleAsync(requests.TrainerId.Value,UserRoleInt.Trainer))
                throw new NoPermissionException("Only users with Trainer role can create appointments.");

            var entity = _mapper.Map<TrainerAppointment>(requests);
            await _trainerAppo.AddAsync(entity);
        }

        public async Task UpdateAsync(TrainerAppoRequests requests)
        {
            if (!requests.Id.HasValue)
                throw new DataValidationException("Id must be set");

            var entity = await _trainerAppo.FindNoTrackingAsync(requests.Id.Value);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            if (!await _userManager.CheckRoleAsync(requests.TrainerId.Value, UserRoleInt.Trainer))
                throw new NoPermissionException("Only users with Trainer role can Update appointments.");

            entity = _mapper.Map<TrainerAppointment>(requests);

            await _trainerAppo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _trainerAppo.FindAsync(id);
            if (entity is null)
                throw new DataNotFoundException("The provided entity is not found");

            await _trainerAppo.DeleteAsync(entity);
        }
    }
}
