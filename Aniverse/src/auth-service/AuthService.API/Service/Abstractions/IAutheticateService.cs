using AuthService.API.DTO_s.Login;
using AuthService.API.DTO_s.Register;
using AuthService.API.DTO_s.TOken;

namespace AuthService.API.Service.Abstractions
{
    public interface IAutheticateService
    {
        public Task<LoginResult> Login(Login login);
        public Task<TokenModel> RefreshToken(TokenModel tokenModel);
        public Task<RegisterResult> Register(Register register);
        public Task CreateRoles();
    }
}
