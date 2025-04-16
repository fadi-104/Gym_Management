using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.IdentityRepository
{
    public interface IAppSignManager
    {
        Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
        Task<TokenResponse> GenerateUserTokens(AppUser user);
    }
}
