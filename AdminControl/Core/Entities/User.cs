namespace AdminControl.Core.Entities
{
    public class User
    {
        public Guid? VendorID { get; set; }   // Nullable GUID (can be NULL in DB)
        public Guid UserID { get; set; }      // Primary key, usually required
        public string? Username { get; set; } // Nullable string
        public string? FullName { get; set; } // Nullable string
        public string? Email { get; set; }    // Nullable string
        public string? Password { get; set; } // Nullable string (hash later)
        public string? UserRole { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? ProfilePictureFileName { get;set; }
    }

}
