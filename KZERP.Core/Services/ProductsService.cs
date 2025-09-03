using KZERP.Core.DTOs.ProductDTO;
using KZERP.Core.Entities.Products;
using KZERP.Core.Interfaces.IProductRepository;
using KZERP.Core.Interfaces.IProductService;


namespace KZERP.Core.Services.ProductsService
{

    public class ProductsService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductsService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");

            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                Code = productDTO.Code,
                Name = productDTO.Name,
                Category = productDTO.Category,
                IsActive = productDTO.IsActive,
            };

            if (string.IsNullOrEmpty(productDTO.Name))
                throw new ArgumentException("Product name required");

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
