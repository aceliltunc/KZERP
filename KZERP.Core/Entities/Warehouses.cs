using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KZERP.Core.Entities.Warehouses
{
    public class Warehouse
    {
        [Key]
        [Column("WarehouseId")]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}