namespace Ordermanagement.Models
{
    public class User
    {
        public Guid VendorID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
