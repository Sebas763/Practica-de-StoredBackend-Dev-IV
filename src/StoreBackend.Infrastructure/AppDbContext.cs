using Microsoft.EntityFrameworkCore;
using StoreBackend.Domain.Entities;   // ← necesario porque User ahora está en ese namespace

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
}