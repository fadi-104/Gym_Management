using Azure.Core;
using BusinessLogicLayer.TrainerProfileServices;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Management.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrainerProfileController : ControllerBase
    {
        private readonly ITrainerProfileService _trainerProfileService;

        public TrainerProfileController(ITrainerProfileService trainerProfileService)
        {
            _trainerProfileService = trainerProfileService;
        }

        /// <summary>
        /// استرجاع بروفايل مدرب حسب id المدرب
        /// </summary>
        /// <param name="TrainerId"></param>
        /// <returns></returns>
        [HttpGet("{TrainerId}")]
        public async Task<IActionResult> Get(int TrainerId)
        {
            var response = await _trainerProfileService.GetAsync(TrainerId);
            return Ok(ApiResponse<TrainerProfileResponses>.SuccessResult(response));
        }


        /// <summary>
        /// تعديل بيانات البروفايل
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] TrainerProfileRequests requests)
        {
            await _trainerProfileService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        
    }
}
