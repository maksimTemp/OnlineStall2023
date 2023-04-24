using DeliveryAPI.Domain;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Messages;

namespace DeliveryAPI.DataContext
{
    public class DeliveryDataContext : DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        public DeliveryDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Delivery>()
                .HasMany(o => o.Items)
            .WithOne(oi => oi.Delivery);

            modelBuilder.Entity<DeliveryItem>()
                .HasOne(x => x.Delivery)
                .WithMany(o => o.Items)
                .HasForeignKey(x => x.DeliveryId);

            modelBuilder.Entity<DeliveryItem>()
                .HasKey(oi => new { oi.DeliveryId, oi.ProductId });

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
