using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserManagement.Core.Entities;

namespace UserManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

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

            // Simple hardcoded check (replace with real authentication logic)
            if (Input.Username == "admin" && Input.Password == "password123")
            {
                // Redirect to home on success
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();

        }
        public IActionResult OnPostLogin()
        {
            return RedirectToPage("/");
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
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }=string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }=string.Empty;
    }
}
