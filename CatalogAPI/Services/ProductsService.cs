using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using CatalogAPI.Domain;
using CatalogAPI.Models.Requests;
using AutoMapper;
using MassTransit;
using SharedLibrary.Messages;

namespace CatalogAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly CatalogDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsService(CatalogDataContext dbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
            var updateMessage = _mapper.Map<ItemChangedMessage>(upd.Entity);
            await _publishEndpoint.Publish(updateMessage);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var deletedEntity = _dbContext.Products.Remove(toDelete);
            var deleteMessage = _mapper.Map<ProductDeletedMessage>(deletedEntity.Entity);
            await _publishEndpoint.Publish(deleteMessage);
            return await _dbContext.SaveChangesAsync();
        }
    }

    public interface IProductsService : IService<Product>
    {
        Task<Product> CreateAsync(ProductCreateRequest product);
    }
}
