using System.Globalization;
using BusinessLogicLayer.TrainerAppointmentService;
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
    public class TrainerAppointment : ControllerBase
    {
        private readonly ITrainerAppoService _trainerAppoService;

        public TrainerAppointment(ITrainerAppoService trainerAppoService)
        {
            _trainerAppoService = trainerAppoService;
        }

        /// <summary>
        /// استرجاع بيانات مواعيد المدرب بشكل مقسم وبناء على id المدرب
        /// </summary>
        /// <param name="options"></param>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        [HttpPost("{trainerId}")]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options, int trainerId)
        {
            var responses = await _trainerAppoService.GetAllAsync(options, trainerId);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<TrainerAppoResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
            };

            return Ok(ApiResponse<PagedResponse<List<TrainerAppoResponses>>>.SuccessResult(newResponse));
        }

        /// <summary>
        /// استرجاع بيانات موعد بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _trainerAppoService.GetAsync(id);
            return Ok(ApiResponse<TrainerAppoResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة موعد جديد
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrainerAppoRequests requests)
        {
            await _trainerAppoService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// تعديل موعد
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TrainerAppoRequests requests)
        { 
            await _trainerAppoService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف موعد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trainerAppoService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
