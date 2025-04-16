using Azure;
using Azure.Core;
using BusinessLogicLayer.Constants;
using BusinessLogicLayer.ExerciseCategoryService;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService category)
        {
            _categoryService = category;
        }

        /// <summary>
        /// استرجاع بيانات فئات التمارين
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _categoryService.GetAllAsync();
            return Ok(ApiResponse<List<ExerciseCategoryResponses>>.SuccessResult(responses));
        }

        /// <summary>
        /// استرجاع بيانات فئة بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _categoryService.GetAsync(id);
            return Ok(ApiResponse<ExerciseCategoryResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة فئة تمارين جديدة
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExerciseCategoryRequests requests)
        {
            await _categoryService.CreateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));

        }

        /// <summary>
        /// تعديل فئة
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ExerciseCategoryRequests requests)
        {
            await _categoryService.UpdateAsync(requests);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// حذف فئة 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
