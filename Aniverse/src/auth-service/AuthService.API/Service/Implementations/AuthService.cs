using AuthService.API.DataAccessLayer.Entites;
using AuthService.API.DTO_s.Login;
using AuthService.API.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.API.Service.Implementations
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<LoginResult> Login(Login login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user is null) throw new Exception("User not found");
            if (!await _userManager.CheckPasswordAsync(user, login.Password)) throw new UnauthorizedAccessException();
            var roles = _userManager.GetRolesAsync(user).Result;
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                };
            authClaims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            var token = _tokenService.CreateToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_config["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            return new LoginResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}
