using ZeissProducts.Data.Models;

namespace ZeissProducts.Data.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductsAsync();
        public Task<Product?> GetProductByIdAsync(int id);
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public void DeleteProduct(Product product);
        bool IsProductAvailable(string ProductName);
        bool IsProductAvailable(int id);
    }
}
