using KZERP.Core.DTOs.ProductDTO;
using KZERP.Core.Entities.Products;

namespace KZERP.Core.Interfaces.IProductService
{

    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}