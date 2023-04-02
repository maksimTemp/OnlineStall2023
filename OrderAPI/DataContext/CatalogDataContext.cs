using OrderAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.DataContext
{
    public class CatalogDataContext : DbContext
    {
        public DbSet<Order> Producers { get; set; }
        public DbSet<OrderItem> Products { get; set; }

        public CatalogDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItems);
        }
    }
}
