using BusinessLogicLayer.Constants;
using BusinessLogicLayer.IdentityService;
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

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }
        /// <summary>
        /// استرجاع بيانات المستخدمين بشكل مقسم بناء على الدور واذا كان مفعل ام لا
        /// </summary>
        /// <param name="options"></param>
        /// <param name="role"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] TableOptions options, string role, bool? isActive)
        {
            var responses = await _userService.GetAllAsync(options, role, isActive);

            var totalPages = responses.TotalCount / (double)options.PageSize;
            var fromItems = options.Skip + 1;
            var toItems = options.Skip + options.PageSize;

            var newResponse = new PagedResponse<List<UserResponses>>()
            {
                Data = responses.Data,
                TotalCount = responses.TotalCount,
                TotalPages = (int)Math.Ceiling(totalPages),
                FromItems = fromItems,
                ToItems = toItems > responses.TotalCount ? responses.TotalCount : toItems,
                PageSize = options.PageSize,
            };

            return Ok(ApiResponse<PagedResponse<List<UserResponses>>>.SuccessResult(newResponse));
        }


        /// <summary>
        /// استرجاع بيانات مستخدم بناء على ال id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _userService.GetAsync(id);
            return Ok(ApiResponse<UserResponses>.SuccessResult(response));
        }

        /// <summary>
        /// اضافة مستخدم جديد
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UserRequests request)
        {
            ModelState.Remove(nameof(request.ImageFile));
            if (!ModelState.IsValid)
            {
                var error = new Dictionary<string, string>
                {
                    ["General"] = ModelState.FirstOrDefault().Value.Errors[0].ErrorMessage
                };
                return BadRequest(ApiResponse<object>.ErrorResult(error));
            }

            await _userService.CreateAsync(request);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        /// تعديل بيانات مستخدم جديد
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UserRequests request)
        {
            ModelState.Remove(nameof(request.ImageFile));
            if (!ModelState.IsValid)
            {
                var error = new Dictionary<string, string>
                {
                    ["General"] = ModelState.FirstOrDefault().Value.Errors[0].ErrorMessage
                };
                return BadRequest(ApiResponse<object>.ErrorResult(error));
            }

            await _userService.UpdateAsync(request);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }

        /// <summary>
        ///لا تحذف بيانات المستخدم من قاعدة البيانات بل تغير حقل النشط لغير نشط
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(null));
        }
    }
}
