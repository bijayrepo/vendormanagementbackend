using Microsoft.EntityFrameworkCore;

namespace VendorWebAPI.Data
{
    public class ServicesDbContext: DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
        {
        }

    }
}
