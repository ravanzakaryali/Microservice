using AuthService.API.Common;
using AuthService.API.DataAccessLayer.Entites;
using AuthService.API.DTO_s.Login;
using AuthService.API.DTO_s.Register;
using AuthService.API.DTO_s.TOken;
using AuthService.API.Service.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.API.Service.Implementations
{
    public class AutheticateService : IAutheticateService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AutheticateService(UserManager<AppUser> userManager,
                           ITokenService tokenService,
                           IConfiguration config,
                           RoleManager<IdentityRole> roleManager,
                           IMapper mapper,
                           SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _config = config;
            _roleManager = roleManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<LoginResult> Login(Login login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user is null) throw new Exception("User not found");
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) throw new UnauthorizedAccessException();

            var roles = _userManager.GetRolesAsync(user).Result;
            List<Claim> authClaims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            authClaims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            var token = _tokenService.CreateToken(authClaims);

            //var refreshToken = _tokenService.GenerateRefreshToken();
            //_ = int.TryParse(_config["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            //user.RefreshToken = refreshToken;
            //await _userManager.UpdateAsync(user);

            return new LoginResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
                //RefreshToken = refreshToken,
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
        public async Task<RegisterResult> Register(Register register)
        {
            RegisterResult registerResult = new();
            AppUser isEmail = await _userManager.FindByNameAsync(register.Email);
            if (isEmail != null) throw new Exception("Already exception");
            AppUser user = _mapper.Map<AppUser>(register);
            user.UserName = await GenerateUsername($"{register.Name} + {register.Surname}");
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return registerResult;
            };
            registerResult.Username = user.UserName;
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            return registerResult;
        }
        private async Task<string> GenerateUsername(string fullname)
        {
            string username = Helper.GeneratorString(fullname);
            AppUser isUserName = await _userManager.FindByNameAsync(username);
            if (isUserName != null)
            {
                await GenerateUsername(fullname);
            }
            return username;
        }
        #region CreateRoles
        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!(await _roleManager.RoleExistsAsync(item.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                }
            }
        }
        #endregion
    }
}
