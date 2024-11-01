using Microsoft.EntityFrameworkCore;
using MvvmPresentation.Data.EF.Model;

namespace MvvmPresentation.Data.EF
{
    internal class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
           
        }

        public DbSet<OrderEntity>? Orders { get; set; }

        public DbSet<CustomerEntity>? Customers { get; set; }
    }
}
