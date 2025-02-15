using ZeissProducts.Business.Models;
using ZeissProducts.Data.Models;
using ZeissProducts.Data.Repositories;

namespace ZeissProducts.Business.Services.Products
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ZeissResult<Product>> AddProductAsync(Product product)
        {
            if (product is null) 
                return new ZeissError("Product details has to be provided.");

            if(string.IsNullOrEmpty(product.Name))
                return new ZeissError("Product name is required to enroll product.");
            
            bool isProductAvailable = _productRepository.IsProductAvailable(product.Name);

            if (isProductAvailable)
                return new ZeissError("Product is already available.");

            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task<ZeissResult<Product>> AddProductStocksAsync(int id, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is null)
                return new ZeissError($"Product ({id}) not found. Please add the product details.");

            product.Inventory += quantity;

            await _productRepository.UpdateProductAsync(product);

            return product;

        }

        public async Task<ZeissResult<Product>> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is null)
                return new ZeissError($"Product ({id}) not found.");

            _productRepository.DeleteProduct(product);

            return ZeissResult<Product>.Success();
        }

        public async Task<ZeissResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is null)
                return new ZeissError($"Product ({id}) not found.");

            return product;
        }

        public async Task<ZeissResult<List<Product>>> GetProductsAsync()
        {
            var products =  await _productRepository.GetProductsAsync();

            if (products is null || products.Count == 0)
                return new ZeissError("No products available");

            return products;
        }

        public async Task<ZeissResult<Product>> RemoveProductStocksAsync(int id, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is null)
                return new ZeissError($"Product({id}) not found. Please add the product details.");

            if (product.Inventory == 0)
                return new ZeissError($"Stock can't be removed. No stock available for this product ({id}).");

            product.Inventory -= quantity;

            await _productRepository.UpdateProductAsync(product);

            return ZeissResult<Product>.Success();
        }

        public async Task<ZeissResult<Product>> UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
                return new ZeissError("Product ID mismatch");

            bool isProductAvailable = _productRepository.IsProductAvailable(id);

            if (!isProductAvailable)
                return new ZeissError($"Product ({id}) not found. Please provide valid product Identifier.");

            await _productRepository.UpdateProductAsync(product);

            return ZeissResult<Product>.Success();


        }
    }
}
