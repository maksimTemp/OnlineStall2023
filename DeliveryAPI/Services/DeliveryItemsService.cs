using AutoMapper;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using DeliveryAPI.Consumers;
using DeliveryAPI.DataContext;
using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;
using SharedLibrary.Messages;

namespace DeliveryAPI.Services
{
    public class DeliveryItemsService : IDeliveryItemService
    {
        private readonly DeliveryDataContext _dbContext;
        private readonly IMapper _mapper;
        public DeliveryItemsService(DeliveryDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeliveryItem>> GetByOrderIdAsync(Guid id)
        {
            return await _dbContext.DeliveryItems
                .Where(x => x.Delivery.Id == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<DeliveryItem>> GetByItemIdAsync(Guid id)
        {
            return await _dbContext.DeliveryItems
                .Where(x => x.ProductId == id)
                .ToListAsync();
        }

        public async Task<DeliveryItem> UpdateAsync(DeliveryItem orderItem)
        {
            var upd = _dbContext.DeliveryItems.Update(orderItem);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task<DeliveryItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task ConsumeItemChangedMessage(ItemChangedMessage message)
        {
            List<DeliveryItem> orderItems = _dbContext.DeliveryItems.Where(x => x.ProductId == message.EntityId).ToList();
            foreach(var upd in orderItems)
            {
                upd.Name = message.Name;
            }
            _dbContext.UpdateRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task ProductDeletedMessageConsume(ProductDeletedMessage message)
        {
            List<DeliveryItem> orderItems = _dbContext.DeliveryItems.Where(x => x.ProductId == message.EntityId).ToList();
            foreach (var upd in orderItems)
            {
                upd.ProductId = Guid.Empty;
                upd.Name = "Product deleted";
            }
            _dbContext.UpdateRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }

    }
    public interface IDeliveryItemService : IService<DeliveryItem>
    {
        Task<IEnumerable<DeliveryItem>> GetByItemIdAsync(Guid id);
        Task<IEnumerable<DeliveryItem>> GetByOrderIdAsync(Guid id);
        Task ConsumeItemChangedMessage(ItemChangedMessage message);
        Task ProductDeletedMessageConsume(ProductDeletedMessage message);
    }
}
