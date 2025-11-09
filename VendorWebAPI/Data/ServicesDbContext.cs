using Microsoft.EntityFrameworkCore;
using VendorWebAPI.Model;

namespace VendorWebAPI.Data
{
    public class ServicesDbContext: DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Vendor)
                .WithMany(v => v.Users)
                .HasForeignKey(u => u.VendorID);
            //.OnDelete(DeleteBehavior.Cascade);//If a Vendor is deleted, all associated Users will also be deleted
        }

    }
}
