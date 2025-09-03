using System.ComponentModel.DataAnnotations;
using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Entities.Products;

namespace KZERP.Core.Entities.InventoryStocks
{
    public class InventoryStock
    {
        public int Id { get; set; }

        Product? ProductId { get; set; }
        Warehouse? WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}