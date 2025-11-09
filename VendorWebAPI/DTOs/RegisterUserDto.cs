using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorWebAPI.DTOs
{
    public class RegisterUserDto
    {
        public Guid VendorID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? UserRole { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? ProfilePictureFileName { get; set; }
        //public Vendor Vendor { get; set; } = new Vendor();
    }
}
