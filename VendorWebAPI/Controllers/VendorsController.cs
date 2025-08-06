using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendorWebAPI.DTOs;
using VendorWebAPI.Interfaces;
using VendorWebAPI.Model;

namespace VendorWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : Controller
    {
        private readonly IVendorService _vendorService;
        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Vendor dto)
        {
            var vendor = await _vendorService.RegisterVendorAsync(dto);
            return Ok(vendor);
        }
    }
}
