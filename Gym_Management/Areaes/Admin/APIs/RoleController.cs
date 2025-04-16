using BusinessLogicLayer.IdentityService;
using Core.Models;
using DomainEntitiesLayer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Management.Areaes.Admin.APIs
{
    [Area("Admin")]
    [Route("[area]/api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// استرجاع كل الادوار
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _roleService.GetAllAsync();
            return Ok(ApiResponse<List<RoleResponses>>.SuccessResult(responses));
        }

    }
}
