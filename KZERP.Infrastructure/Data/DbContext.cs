
using KZERP.Core.Entities.Products;
using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Entities.InventoryStocks;
using KZERP.Core.Entities.WorkOrders;
using KZERP.Core.Entities.RfidLogs;
using KZERP.Core.Entities.InventoryMovements;


using Microsoft.EntityFrameworkCore;


namespace KZERP.Infrastructure.Data
{

    public class KZERPDbContext : DbContext
    {
        public KZERPDbContext(DbContextOptions<KZERPDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryStock> InventoryStocks { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<RfidLogs> RfidLogs { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
    }

}

