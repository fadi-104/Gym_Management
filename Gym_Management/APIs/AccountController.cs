using BusinessLogicLayer.IdentityService;
using Core.Models;
using DomainEntitiesLayer.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Management.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// تسجيل الدخول بناء على اسم المستخدم وكلمة الرور تعيد توكين يحتوي بيانات المستخدم
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Login([FromBody] DomainEntitiesLayer.Requests.LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            return ApiResponse<TokenResponse>.SuccessResult(result);
        }

    }
}
