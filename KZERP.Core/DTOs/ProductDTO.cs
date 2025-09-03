using System.ComponentModel.DataAnnotations;

namespace KZERP.Core.DTOs.ProductDTO
{
    public class ProductDTO
    {
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
    }
}