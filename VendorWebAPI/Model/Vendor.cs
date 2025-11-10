using System.ComponentModel.DataAnnotations;

namespace VendorWebAPI.Model
{
    public class Vendor
    {
        [Key]
        public Guid VendorID { get; set; } = Guid.NewGuid();
        //[Required]
        public string CompanyName { get; set; }=string.Empty;
        //[Required]
        public string ContactName { get; set; } = string.Empty;
        //[Required]
        public string ContactEmail { get; set; } = string.Empty;
        //[Required]
        public string ContactPhone { get; set; } = string.Empty;
        //[Required]
        public string Address { get; set; } = string.Empty;
        //[Required]
        public string City { get; set; } = string.Empty;
        //[Required]
        public string State { get; set; } = string.Empty;
        //[Required]
        public string Country { get; set; } = string.Empty;
        //[Required]
        public string GSTNumber { get; set; } = string.Empty;
        //[Required]
        public string PANNumber { get; set; } = string.Empty;
        //[Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //[Required]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
