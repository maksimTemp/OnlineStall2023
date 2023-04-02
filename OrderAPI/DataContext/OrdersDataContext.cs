using OrderAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.DataContext
{
    public class OrdersDataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrdersDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItems);
        }
    }
}
