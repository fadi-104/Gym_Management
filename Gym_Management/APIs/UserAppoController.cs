using BusinessLogicLayer.UserAppoServices;
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
    public class UserAppoController : ControllerBase
    {
        private readonly IUserAppoService _userAppoService;

        public UserAppoController(IUserAppoService userAppoService)
        {
            _userAppoService = userAppoService;
        }
        /// <summary>
        /// استرجاع المواعيد التي حجزها المتدرب عند المدرب بناء على id االمتدرب 
        /// ...
        /// امكانية الفلترة حسب التاربخ
        /// تمكن المتدرب من رؤية مواعيده التي حجزها
        /// </summary>
        /// <param name="traineeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost("{traineeId}")]
        public async Task<IActionResult> GetForTrainee(int traineeId, [FromBody] DateTime date)
        {
            var responses = await _userAppoService.GetAllForTraineeAsync(traineeId, date);
            return Ok(ApiResponse<List<UserAppointmentResponses>>.SuccessResult(responses));
        }

        /// <summary>
        /// استرجاع البيانات التي حجزها المتدرب بناء على id المدرب
        /// ...
        /// امكانية الفلترة حسب التاريخ
        /// تمكن المدرب من رؤية جميع مواعيده التي حجزها المتدربين
        /// </summary>
        /// <param name="trainerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost("{trainerId}")]
        public async Task<IActionResult> GetForTrainer(int trainerId, [FromBody] DateTime date)
        {
            var responses = await _userAppoService.GetAllForTrainerAsync(trainerId, date);
            return Ok(ApiResponse<List<UserAppointmentResponses>>.SuccessResult(responses));
        }

        /// <summary>
        /// استرجاع بيانات موعد محجوز بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _userAppoService.GetAsync(id);
            return Ok(ApiResponse<UserAppointmentResponses>.SuccessResult(response));
        }

        /// <summary>
        /// حجز موعد
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAppointmentRequests requests)
        {
            await _userAppoService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// الغاء موعد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userAppoService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
