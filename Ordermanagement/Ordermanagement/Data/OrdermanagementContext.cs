using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordermanagement.Models;

namespace Ordermanagement.Data
{
    public class OrdermanagementContext : DbContext
    {
        public OrdermanagementContext (DbContextOptions<OrdermanagementContext> options)
            : base(options)
        {
        }

        public DbSet<Ordermanagement.Models.Vendor> Vendor { get; set; } = default!;
    }
}
