using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data {

    /// <summary>
    /// Database context for the application, utilizing Entity Framework Core.
    /// </summary>
    public class AppDbContext : DbContext {

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}