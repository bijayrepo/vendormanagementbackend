using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;

namespace UserManagement.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _IUserService;
        public IndexModel(IUserService userService)
        {
            _IUserService = userService ?? throw new ArgumentNullException(nameof(_IUserService));
        }
        public User Input { get; set; } = new User();
        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                string _userid = HttpContext.Session.GetString("UserId");
                Input.UserID = Guid.TryParse(_userid, out Guid userId) ? userId : Guid.Empty;
                Input = _IUserService.GetUserByIdAsync(Input.UserID).Result ?? new User();
            }
            else
            {
                RedirectToPage("/Account/Login");
            }
        }
    }
}
