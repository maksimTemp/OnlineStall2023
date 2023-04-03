using UserAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace UserAPI.DataContext
{
    public class UsersDataContext : DbContext
    {
        public DbSet<Identity> Identities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public UsersDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Identity);
            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
