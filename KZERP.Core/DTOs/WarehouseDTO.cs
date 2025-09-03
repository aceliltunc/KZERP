using System.ComponentModel.DataAnnotations;

namespace KZERP.Core.DTOs.WarehouseDTO
{
    public class WarehouseDTO
    {
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}