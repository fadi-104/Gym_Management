using BusinessLogicLayer.TraineeDataServices;
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
    public class TraineeDataController : ControllerBase
    {
        private readonly ITraineeDataService _traineeDataService;
        public TraineeDataController(ITraineeDataService traineeDataService)
        {
            _traineeDataService = traineeDataService;
        }

        /// <summary>
        /// استرجاع بيانات متدرب بناء على ال id 
        /// </summary>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        [HttpGet("{traineeId}")]
        public async Task<IActionResult> Get(int traineeId)
        {
            var response = await _traineeDataService.GetAsync(traineeId);
            return Ok(ApiResponse<TraineeDataResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة بيانات للمتدرب
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TraineeDataRequests requests)
        {
            await _traineeDataService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// تعديل بيانات متدرب
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TraineeDataRequests requests)
        {
            await _traineeDataService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف بيانات متدرب
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _traineeDataService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
 
    }
}
