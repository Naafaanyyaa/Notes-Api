using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using FirstLab.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstLab.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-account")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest userModel)
        {
            var result = await _userService.RegisterAsync(userModel);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpPost("login-account")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest userLogin)
        {
            var result = await _userService.LoginAsync(userLogin);
            return result.IsAuthSuccessful ? Ok(result) : Unauthorized(result);
        }

        [HttpGet("get-user-list")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserListAsync()
        {
            var result = await _userService.GetUserList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(ChangePasswordRequest user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var userName = User.FindFirstValue(ClaimTypes.Name)!;
            var result = await _userService.ChangePasswordAsync(user, userId, userName);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }

}