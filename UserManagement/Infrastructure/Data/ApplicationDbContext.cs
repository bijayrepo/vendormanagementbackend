using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

    }
}
