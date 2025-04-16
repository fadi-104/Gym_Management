using Core.Models;
using DomainEntitiesLayer.Requests;
using DomainEntitiesLayer.Responses;

namespace BusinessLogicLayer.IdentityService
{
    public interface IUserService
    {
        Task<UserResponses> GetAsync(int id);
        Task CreateAsync(UserRequests requests);
        Task UpdateAsync(UserRequests requests);
        Task DeleteAsync(int id);
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<PagedResponse<List<UserResponses>>> GetAllAsync(TableOptions options, string role, bool? isActive);
    }
}
