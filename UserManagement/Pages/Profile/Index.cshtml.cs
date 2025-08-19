using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
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
            string _userid=string.Empty;
            if (User.Identity?.IsAuthenticated ?? false)
            {
                _userid = User.Identity.Name ?? "";
                var claim = User.FindFirst("UserId");
                if (claim != null)
                {
                    _userid = claim.Value;
                }
                //foreach (var claim in User.Claims)
                //{
                //    if (claim.Type == ClaimTypes.Name)
                //    {
                //        _userid = claim.Value;
                //        break;
                //    }
                //}
                //string FullName = User.FindFirst("UserId")?.Value ?? "";
                //_userid = FullName;
                Input.UserID = Guid.TryParse(_userid, out Guid userId) ? userId : Guid.Empty;
                Input = _IUserService.GetUserByIdAsync(Input.UserID).Result ?? new User();
            }
            //if (HttpContext.Session.GetString("UserId") != null)
            //{
                
            //    //_userid = HttpContext.Session.GetString("UserId");
            //    Input.UserID = Guid.TryParse(_userid, out Guid userId) ? userId : Guid.Empty;
            //    Input = _IUserService.GetUserByIdAsync(Input.UserID).Result ?? new User();
            //}
            else
            {
                RedirectToPage("/Account/Login");
            }
        }
    }
}
