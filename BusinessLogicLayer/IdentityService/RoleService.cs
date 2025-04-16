using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Mapping;
using DataAccessLayer.IdentityRepository;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.IdentityService
{
    public class RoleService : IRoleService
    {
        private readonly IAppRoleManager _roleManager;
        private readonly IMapper _mapper;
        public RoleService(IAppRoleManager appRole, IMapper mapper)
        {
            _roleManager = appRole;
            _mapper = mapper;
        }

        public async Task<List<RoleResponses>> GetAllAsync()
        {
            var list = await _roleManager.GetAllAsync();
            var responses = list.Select(x => _mapper.Map<RoleResponses>(x)).ToList();

            return responses;
        }
    }
}
