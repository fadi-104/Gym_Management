using Azure.Core;
using BusinessLogicLayer.TrainerSubServices;
using Core.Models;
using DataAccessLayer.Repository.TrainerSubscriptions;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Management.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrainerSubController : ControllerBase
    {
        private readonly ITrainerSubService _trainerSubService;
        public TrainerSubController(ITrainerSubService trainerSubService)
        {
            _trainerSubService = trainerSubService;
        }

        /// <summary>
        /// استرجاع جميع المشتكرين بشكل مقسم مع مدرب ما بناء على id المدرب 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        [HttpPost("{trainerId}")]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options, int trainerId)
        {
            var responses = await _trainerSubService.GetAllAsync(options, trainerId);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<TrainerSubscriptionResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize,
            };

            return Ok(ApiResponse<PagedResponse<List<TrainerSubscriptionResponses>>>.SuccessResult(newResponse));
        }


        /// <summary>
        /// استرجاع اشتراك بناء على ال id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _trainerSubService.GetAsync(id);
            return Ok(ApiResponse<TrainerSubscriptionResponses>.SuccessResult(response));
        }

        /// <summary>
        /// لعرض المدرب الذي تم الاشتراك معه بناء على id المتدرب
        /// </summary>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        [HttpGet("{traineeId}")]
        public async Task<IActionResult> GetTrainer(int traineeId)
        {
            var response = await _trainerSubService.GetTrainerAsync(traineeId);
            return Ok(ApiResponse<TrainerSubscriptionResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اشتراك مع مدرب
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrainerSubscriptionRequests requests)
        {
            await _trainerSubService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف مدرب
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trainerSubService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
