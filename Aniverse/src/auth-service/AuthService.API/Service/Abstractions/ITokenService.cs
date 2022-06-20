using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.API.Service.Abstractions
{
    public interface ITokenService
    {
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public string GenerateRefreshToken();
    }
}
