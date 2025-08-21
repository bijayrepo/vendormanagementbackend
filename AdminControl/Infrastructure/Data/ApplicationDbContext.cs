using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using AdminControl.Core.Entities;

namespace AdminControl.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;

    }
}
