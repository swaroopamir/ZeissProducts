using ZeissProducts.Business.Models;
using ZeissProducts.Data.Models;

namespace ZeissProducts.Business.Services.Products
{
    public interface IProductService
    {
        public Task<ZeissResult<List<Product>>> GetProductsAsync();
        public Task<ZeissResult<Product>> GetProductByIdAsync(int id);
        public Task<ZeissResult<Product>> AddProductAsync(Product product);
        public Task<ZeissResult<Product>> UpdateProductAsync(int id, Product product);
        public Task<ZeissResult<Product>> AddProductStocksAsync(int id, int quantity);
        public Task<ZeissResult<Product>> RemoveProductStocksAsync(int id, int quantity);
        public Task<ZeissResult<Product>> DeleteProductAsync(int id);
    }
}
