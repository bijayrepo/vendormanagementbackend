using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendorWebAPI.DTOs;
using VendorWebAPI.Interfaces;

namespace VendorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid user data.");
            }
            var result = await _userService.RegisterUserAsync(dto);
            if (!result)
            {
                return Conflict("Email is already taken.");
            }
            return Ok("User registered successfully.");
        }
    }
}
