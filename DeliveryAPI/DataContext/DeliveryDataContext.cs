using DeliveryAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.DataContext
{
    public class DeliveryDataContext : DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        public DeliveryDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>()
                .HasMany(x => x.Items);
        }
    }
}
