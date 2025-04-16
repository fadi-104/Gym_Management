using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.TrainerAppointmentService
{
    public interface ITrainerAppoService
    {
        Task CreateAsync(TrainerAppoRequests requests);
        Task DeleteAsync(int id);
        Task<PagedResponse<List<TrainerAppoResponses>>> GetAllAsync(TableOptions options, int trainerId);
        Task<TrainerAppoResponses> GetAsync(int id);
        Task UpdateAsync(TrainerAppoRequests requests);
    }
}
