using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KZERP.Core.Entities.Products
{
    public class Product
    {
        [Key]
        [Column("ProductId")]
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
    }
}