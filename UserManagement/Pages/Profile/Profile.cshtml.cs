using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Core.Entities;

namespace UserManagement.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        //public ProfileModel(UserManager<IdentityUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public void OnGet()
        {
        }
        
        [BindProperty]
        public User Input { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            //var user = await _userManager.GetUserAsync(User);
            //user.UserName = "vgg";
            User user = new User();
            user.FullName = "Bijay";
            user.Email = "biay@gmail.com";



            if (user != null)
            {
                Input = new User
                {
                    Username = user.FullName,
                    Email = user.Email,
                    FullName = user.FullName // replace with custom FullName if stored
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");

            // Update profile info
            user.Email = Input.Email;
            // If you have a FullName property in your user table, update it here

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Message = "Profile updated successfully!";
                return Page();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
