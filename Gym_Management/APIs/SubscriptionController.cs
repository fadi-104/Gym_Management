using BusinessLogicLayer.SubscriptionServices;
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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionServices _subscriptionServices;

        public SubscriptionController(ISubscriptionServices subscriptionServices)
        {
            _subscriptionServices = subscriptionServices;
        }

        /// <summary>
        /// استرجاع بيانات الاشتراكات بشكل مقسم
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options)
        {
            var responses = await _subscriptionServices.GetAllAsync(options);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<SubscriptionResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize,
            };

            return Ok(ApiResponse<PagedResponse<List<SubscriptionResponses>>>.SuccessResult(newResponse));
        }

        /// <summary>
        /// استرجاع بيانات اشتراك بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _subscriptionServices.GetAsync(id);
            return Ok(ApiResponse<SubscriptionResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة اشتراك من خلال اختيار خطة
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionRequests requests)
        {
            await _subscriptionServices.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }


        /// <summary>
        /// حذف اشتراك (الغاء اشتراك)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscriptionServices.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
