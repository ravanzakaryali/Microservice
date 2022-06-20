using AuthService.API.DTO_s.Login;
using AuthService.API.DTO_s.TOken;

namespace AuthService.API.Service.Abstractions
{
    public interface IAuthService
    {
        public Task<LoginResult> Login(Login login);
        public Task<TokenModel> RefreshToken(TokenModel tokenModel);
        public Task CreateRoles();
    }
}
