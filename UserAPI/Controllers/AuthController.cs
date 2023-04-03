using AuthorizationApi.Services;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models.Requests;

namespace AuthorizationApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService tokenService)
        {
            _authService = tokenService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.LogInAsync(request);
            return result.Success ? 
                Ok(result) : 
                Unauthorized(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.RegisterAsync(request);
            return result.Success ? 
                Ok(result) : 
                Unauthorized(result);
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.RefreshAsync(request);
            return result.Success ?
                Ok(result) :
                Unauthorized(result);
        }

    }
}
