using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _IUserService;
        public AccountController(ApplicationDbContext context, IUserService iuserService)
        {
            _context = context;
            _IUserService = iuserService ?? throw new ArgumentNullException(nameof(iuserService));
        }
        public LoginViewModel Input { get; set; } = new LoginViewModel();
        public string Message { get; set; } = string.Empty;
        [HttpGet]
        public IActionResult Login()
        {
            return View(Input);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = _IUserService.LoginAsync(Input.Username, Input.Password).Result;
            bool isValidUser = (loginViewModel.Username == "admin" && loginViewModel.Password == "password");
            if (isValidUser)
            {
                return RedirectToAction("Register", "Account");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(loginViewModel);
        }
        public IActionResult Logout()
        {
            // Clear the session and sign out
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        } 
        public IActionResult Register()
        {
            return View();
        }
    }
}
