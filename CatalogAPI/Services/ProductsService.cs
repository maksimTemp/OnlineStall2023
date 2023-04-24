using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using CatalogAPI.Domain;
using CatalogAPI.Models.Requests;
using AutoMapper;
using MassTransit;
using SharedLibrary.Messages;
using Microsoft.EntityFrameworkCore.Query;

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
            updateMessage.OperationType = SharedLibrary.Enums.OperationTypeMessage.Update;
            await _publishEndpoint.Publish(updateMessage);
            await _dbContext.SaveChangesAsync();
            return upd.Entity;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var deletedEntity = _dbContext.Products.Remove(toDelete);
            var deleteMessage = _mapper.Map<ProductDeletedMessage>(deletedEntity.Entity);
            await _publishEndpoint.Publish(deleteMessage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> ChangeStockQuantityMessageConsume(ChangeStockQuantityMessage message)
        {
            foreach (var item in message.ProductsQuantity)
            {
                var prod = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.Id);
                prod.Quantity -= item.Quantity;
                _dbContext.Products.Update(prod);
            }
            return await _dbContext.SaveChangesAsync();
        }
    }

    public interface IProductsService : IService<Product>
    {
        Task<int> ChangeStockQuantityMessageConsume(ChangeStockQuantityMessage message);
        Task<Product> CreateAsync(ProductCreateRequest product);
    }
}
