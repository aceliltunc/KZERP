using System.ComponentModel.DataAnnotations;
using KZERP.Core.Entities.Products;
using KZERP.Core.Entities.Warehouses;

namespace KZERP.Core.Entities.InventoryMovements
{
    public class InventoryMovement
    {
        public int Id { get; set; }

        Product? ProductId { get; set; }

        Warehouse? WarehouseId { get; set; }
        // Giriş 'IN', Çıkış 'OUT'
        public string? MovementType { get; set; }
        public int Quantity { get; set; }
        [Required]
        [MaxLength(64)]
        public string? RfidTagId { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
    }
}