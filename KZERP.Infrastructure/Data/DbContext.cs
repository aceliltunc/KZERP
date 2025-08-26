
using KZERP.Core;
using Microsoft.EntityFrameworkCore;

namespace KZERP.Infrastructure.Data;

public class KZERPDbContext : DbContext
{
    public KZERPDbContext(DbContextOptions<KZERPDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<InventoryStock> InventoryStocks { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
        
    //     modelBuilder.Entity<Product>().ToTable("Products");
    //     base.OnModelCreating(modelBuilder);
    // }
}
