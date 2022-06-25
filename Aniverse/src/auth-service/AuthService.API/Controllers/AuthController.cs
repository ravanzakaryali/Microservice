using AuthService.API.DTO_s.Login;
using AuthService.API.DTO_s.Register;
using AuthService.API.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutheticateService _service;

        public AuthController(IAutheticateService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                return Ok(await _service.Login(login));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(Register register)
        {
            try
            {
                return Ok(await _service.Register(register));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("role")]
        public async Task<ActionResult> CreateRole()
        {
            try
            {
                await _service.CreateRoles();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
