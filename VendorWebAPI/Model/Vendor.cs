namespace VendorWebAPI.Model
{
    public class Vendor
    {
        public Guid VendorID { get; set; } = Guid.NewGuid();

        public string CompanyName { get; set; }=string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string GSTNumber { get; set; } = string.Empty;

        public string PANNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

    }
}
