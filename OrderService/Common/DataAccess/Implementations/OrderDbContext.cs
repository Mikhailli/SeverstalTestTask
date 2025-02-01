using System.Runtime.CompilerServices;
using Common.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.Implementations;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var path = Path.Combine(Directory.GetParent(GetSourceFilePath())!.Parent!.FullName, "order.db");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    private string GetSourceFilePath([CallerFilePath] string? filePath = null)
    {
        if ( filePath is null)
        {
            throw new Exception("Не найден исходный файл");
        }
        return filePath;
    }
}
