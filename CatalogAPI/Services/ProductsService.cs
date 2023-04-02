using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using CatalogAPI.Domain;

namespace CatalogAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly CatalogDataContext _dbContext;


        public ProductsService(CatalogDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {

            var res = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var upd = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Products.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }

    public interface IProductsService : IService<Product>
    {
        Task<Product> CreateAsync(Product product);
        Task UpdateRangeAsync(IEnumerable<Product> product);
    }
}
