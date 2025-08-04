using Microsoft.EntityFrameworkCore;
using VendorWebAPI.Model;

namespace VendorWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
