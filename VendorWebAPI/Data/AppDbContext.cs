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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Model.User>().HasKey(u => u.Id);
        //    modelBuilder.Entity<Model.User>().Property(u => u.Username).IsRequired().HasMaxLength(100);
        //    modelBuilder.Entity<Model.User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        //    modelBuilder.Entity<Model.User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
        //}
    }
}
