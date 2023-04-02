using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using CatalogAPI.Domain;
using CatalogAPI.Models.Requests;
using AutoMapper;

namespace CatalogAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly CatalogDataContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsService(CatalogDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> CreateAsync(ProductCreateRequest product)
        {
            var toCreate = _mapper.Map<Product>(product);
            var res = await _dbContext.Products.AddAsync(toCreate);
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
        Task<Product> CreateAsync(ProductCreateRequest product);
        Task UpdateRangeAsync(IEnumerable<Product> product);
    }
}
