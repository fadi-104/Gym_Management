using BusinessLogicLayer.ExerciseService;
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
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        /// <summary>
        /// استرجاع بيانات التمارين
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options)
        {
            var responses = await _exerciseService.GetAllAsync(options);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponses = new PagedResponse<List<ExerciseResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize
            };

            return Ok(ApiResponse<PagedResponse<List<ExerciseResponses>>>.SuccessResult(newResponses));
        }

        /// <summary>
        /// استرجاع بيانات التمارين بناء على اسم الفئة
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetAllByCategory(string categoryName)
        {
            var responses = await _exerciseService.GetAllByCategoryAsync(categoryName);
            return Ok(ApiResponse<List<ExerciseResponses>>.SuccessResult(responses));
        }

        /// <summary>
        /// استرجاع بيانات تمرين بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _exerciseService.GetAsync(id);
            return Ok(ApiResponse<ExerciseResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة تمرين جديد
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExerciseRequests requests)
        {
            await _exerciseService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));

        }

        /// <summary>
        /// تعديل بيانات تمرين
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ExerciseRequests requests)
        {
            ModelState.Remove(nameof(requests.ImageFile));
            await _exerciseService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف تمرين بناء على id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exerciseService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
