using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainEntitiesLayer.Entities;
using DomainEntitiesLayer.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DataAccessLayer.IdentityRepository
{
    public class AppSignInManager : SignInManager<AppUser>, IAppSignManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AppSignInManager(UserManager<AppUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<AppUser> confirmation,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
                  logger, schemes, confirmation
                  )
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            
        }

        public async Task<TokenResponse> GenerateUserTokens(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var role = (await UserManager.GetRolesAsync(user)).FirstOrDefault();

            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier,user.Id.ToString()),
                new (ClaimTypes.Name,user.FirstName),
                new (ClaimTypes.Surname,user.LastName),
                new (ClaimTypes.Email,user.Email ?? ""),
                new (ClaimTypes.Role,role ?? ""),
                new ("Logo", user.Image ?? ""),
            };

            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var key = Encoding.UTF8.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                Issuer = issuer,
                Audience = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid().ToString(),
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = role
            };
        }
    }
}
