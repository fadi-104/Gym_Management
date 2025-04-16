using Azure.Core;
using BusinessLogicLayer.WorkoutServices;
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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }


        /// <summary>
        /// استرجاع بيانات تطور المتدرب بناء على ال id الخاص به
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options, int userId)
        {
            var responses = await _workoutService.GetAllAsync(options,userId);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<WorkoutDataResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize,
            };

            return Ok(ApiResponse<PagedResponse<List<WorkoutDataResponses>>>.SuccessResult(newResponse));
        }

        /// <summary>
        /// استرجاع بيانات محسوبة بمعادلة بالنسبة لكل تاريخ لعرضها في جدول بيانتي
        /// بناء على اسم التمرين و id المتدرب
        /// </summary>
        /// <param name="exerciseName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<IActionResult> GetByExercise([FromBody] string exerciseName, int userId)
        {
            var responses = await _workoutService.GetProgressAsync(exerciseName, userId);
            return Ok(ApiResponse<List<ProgessResponses>>.SuccessResult(responses));
        }

        /// <summary>
        /// استرجاع بيانات تطور ما بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _workoutService.GetAsync(id);
            return Ok(ApiResponse<WorkoutDataResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة بيانات تطور
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutDataRequests requests)
        {
            await _workoutService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// تعديل بيانات تطور
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkoutDataRequests requests)
        {
            await _workoutService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف بيانات التطور
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _workoutService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
