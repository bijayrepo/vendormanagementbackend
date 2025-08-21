using Microsoft.AspNetCore.Mvc;

namespace AdminControl.Controllers
{
    public class VendorController : Controller
    {
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }
    }
}
