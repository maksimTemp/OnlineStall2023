using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.DataContext
{
    public class CatalogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }

        public CatalogDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category);

            base.OnModelCreating(modelBuilder);
        }
    }
}
