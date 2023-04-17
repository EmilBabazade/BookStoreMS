using Microsoft.EntityFrameworkCore;

namespace OrderManagerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Order.Order>()
            .HasMany(o => o.Books)
            .WithOne(b => b.Order)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public virtual DbSet<Order.Order> Orders { get; set; }
    public virtual DbSet<Order.Book> Books { get; set; }
}
