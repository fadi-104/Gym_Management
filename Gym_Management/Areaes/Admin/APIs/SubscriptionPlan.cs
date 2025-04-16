using Azure.Core;
using BusinessLogicLayer.SubscriptionPlanServices;
using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Management.Areaes.Admin.APIs
{
    [Area("Admin")]
    [Route("[area]/api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionPlan : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;
        public SubscriptionPlan(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }


        /// <summary>
        /// استرجاع بيانات خطط الاشتراك بشكل مقسم بناء اذا كانت نشطة ام لا
        /// </summary>
        /// <param name="options"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options, bool? isActive)
        {
            var responses = await _subscriptionPlanService.GetAllAsync(options, isActive);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<SubscriptionPlanResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize
            };

            return Ok(ApiResponse<PagedResponse<List<SubscriptionPlanResponses>>>.SuccessResult(newResponse));
        }

        /// <summary>
        /// استرجاع بيانات خطة اشتراك بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _subscriptionPlanService.GetAsync(id);
            return Ok(ApiResponse<SubscriptionPlanResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة بيانات خطة جديدة
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionPlanRequests requests)
        {
            await _subscriptionPlanService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// تعديل بيانات خطة ما
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SubscriptionPlanRequests requests)
        {
            await _subscriptionPlanService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف خطة
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscriptionPlanService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
