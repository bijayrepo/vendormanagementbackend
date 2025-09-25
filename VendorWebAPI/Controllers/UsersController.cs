using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using VendorWebAPI.DTOs;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;
using VendorWebAPI.Services;

namespace VendorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAuthService _userAuthService;
        private readonly IMemoryCache _cache;
        private readonly IAuthService _iAuthService;

        public UsersController(IUserService userService, IUserAuthService userAuthService,
            IMemoryCache cache, IAuthService iAuthService)
        {
            _userService = userService;
            _userAuthService = userAuthService;
            _cache = cache;
            _iAuthService = iAuthService;
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterUserDto input)
        {
            if (string.IsNullOrEmpty(input.Email) && string.IsNullOrEmpty(input.Password))
            {
                var user = await _userAuthService.UserLogin(input.Email, input.Password);
                return BadRequest(JsonConvert.SerializeObject(user));
            }

            string cacheKey = $"User:{input.Email}";

            // Try to get user from cache
            if (!_cache.TryGetValue(cacheKey, out UserResponse cachedUser))
            {
                // Fetch from database/service
                var user = await _userAuthService.UserLogin(input.Email,input.Password);
                if (user == null || user.data.Password !=input.Password) 
                    return Unauthorized(JsonConvert.SerializeObject(user));

                // Cache the user for 10 minutes (sliding expiration 2 minutes)
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(cacheKey, user, cacheOptions);

                cachedUser = user;
            }

            // Generate JWT token
            var token = "Jwt-token";
            //var token = await _iAuthService.GenerateJwtTokenAsync(cachedUser.data.Email, cachedUser.data.Password);
            

            return Ok(JsonConvert.SerializeObject(cachedUser));
        }
    

}
}
