using AdminControl.Core.Entities;
using AdminControl.Core.Interfaces;
using AdminControl.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminControl.Controllers
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            User user = _IUserService.LoginAsync(loginViewModel.Username, loginViewModel.Password).Result;
            if( user != null)
            {
                TempData["UserData"] = JsonConvert.SerializeObject(user);
                return RedirectToAction("Profile", "Account");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            if (TempData["UserData"] != null)
            {
                var userJson = TempData["UserData"].ToString();
                User user =JsonConvert.DeserializeObject<User>(userJson);
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Profile(User user, string action)
        {
            switch (action)
            {
                case "Update":
                    if (user != null)
                    {
                        var updatedUser = _IUserService.UpdateUserAsync(user).Result;
                        if (updatedUser != null)
                        {
                            TempData["UserData"] = JsonConvert.SerializeObject(updatedUser);
                            return RedirectToAction("Profile", "Account");
                        }
                    }
                    break;

                case "Logout":
                    TempData.Remove("UserData");
                    return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Profile", "Account");
        }
        [HttpGet]
        public IActionResult UpdateUser()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
