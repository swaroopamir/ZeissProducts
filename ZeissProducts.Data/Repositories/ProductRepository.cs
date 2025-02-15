using Microsoft.EntityFrameworkCore;
using ZeissProducts.Data.Models;

namespace ZeissProducts.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ZeissDbContext _dbContext;

        public ProductRepository(ZeissDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _dbContext.Products.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsProductAvailable(string ProductName) 
        {
            return _dbContext.Products.Any(p => p.Name == ProductName.Trim());
        }

        public bool IsProductAvailable(int id) 
        {
            return _dbContext.Products.Any(p => p.Id == id);
        }
    }
}
