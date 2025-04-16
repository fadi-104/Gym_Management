using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository.UserAppo;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.UserAppoServices
{
    public interface IUserAppoService
    {
        Task CreateAsync(UserAppointmentRequests requests);
        Task DeleteAsync(int id);
        Task<List<UserAppointmentResponses>> GetAllForTraineeAsync(int traineeId, DateTime dateTime);
        Task<List<UserAppointmentResponses>> GetAllForTrainerAsync(int trainerId, DateTime date);
        Task<UserAppointmentResponses> GetAsync(int id);
    }
}
