using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _IUserService;
        public LoginModel(ApplicationDbContext context,IUserService iuserService)
        {
            _context = context;
            _IUserService = iuserService ?? throw new ArgumentNullException(nameof(_IUserService));
        }
        [BindProperty]
        public User Input { get; set; } = new();

        public string ErrorMessage { get; set; }=string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IEnumerable<User> temp= _IUserService.GetAllUsersAsync().Result;

           // var user= _IUserService.GetUserByIdAsync(Input.Username).Result;

            var user = _IUserService.LoginAsync(Input.Username, Input.Password).Result;
            if (string.IsNullOrEmpty(Input.Username) || string.IsNullOrEmpty(Input.Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            //var user = _context.Users
            //    .FirstOrDefaultAsync(u => u.Username == Input.Username && u.Password == Input.Password);

            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            HttpContext.Session.SetString("UserId",Convert.ToString(user.UserID));
            return RedirectToPage("/Profile/Index");


        }
       
        public IActionResult OnPostRegister()
        {
            
            return RedirectToPage("/Account/Register");
        }

        public IActionResult OnPostForgot()
        {
            return RedirectToPage("/Account/ForgotPassword");
        }
    }
}
