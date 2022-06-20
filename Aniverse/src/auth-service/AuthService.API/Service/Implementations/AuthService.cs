using AuthService.API.DataAccessLayer.Entites;
using AuthService.API.DTO_s.Login;
using AuthService.API.DTO_s.TOken;
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

        public AuthService(UserManager<AppUser> userManager,
                           ITokenService tokenService,
                           IConfiguration config)
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
            List<Claim> authClaims = new()
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
        public async Task<TokenModel> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null) throw new NullReferenceException(nameof(tokenModel));
            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null) throw new UnauthorizedAccessException();
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new NullReferenceException();
            var newAccessToken = _tokenService.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
    }
}
