using AutoMapper;
using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Consumers;
using OrderAPI.DataContext;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;
using SharedLibrary.Messages;

namespace OrderAPI.Services
{
    public class OrderItemsService : IOrderItemService
    {
        private readonly OrdersDataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderItemsService(OrdersDataContext dbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid id)
        {
            return await _dbContext.OrderItems
                .Where(x => x.Order.Id == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderItem>> GetByItemIdAsync(Guid id)
        {
            return await _dbContext.OrderItems
                .Where(x => x.ProductId == id)
                .ToListAsync();
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem)
        {
            var upd = _dbContext.OrderItems.Update(orderItem);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }
        public async Task<int> DeleteAsync(DeleteOrderItemRequest deleteOrderItemRequest)
        {
            var toDelete = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Order.Id == deleteOrderItemRequest.OrderId && x.ProductId == deleteOrderItemRequest.OrderItemId);
            var deletedEntity = _dbContext.OrderItems.Remove(toDelete);
            var deleteMessage = _mapper.Map<OrderItemDeletedMessage>(deletedEntity.Entity);
            await _publishEndpoint.Publish(deleteMessage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task ConsumeItemChangedMessage(ItemChangedMessage message)
        {
            List<OrderItem> orderItems = _dbContext.OrderItems.Where(x => x.ProductId == message.EntityId).ToList();
            foreach(var upd in orderItems)
            {
                upd.Name = message.Name;
            }
            _dbContext.OrderItems.UpdateRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task ProductDeletedMessageConsume(ProductDeletedMessage message)
        {
            List<OrderItem> orderItems = _dbContext.OrderItems.Where(x => x.ProductId == message.EntityId).ToList();
            foreach (var upd in orderItems)
            {
                upd.ProductId = Guid.Empty;
                upd.Name = "Product deleted";
            }
            _dbContext.UpdateRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }

    }
    public interface IOrderItemService : IService<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByItemIdAsync(Guid id);
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid id);
        Task ConsumeItemChangedMessage(ItemChangedMessage message);
        Task ProductDeletedMessageConsume(ProductDeletedMessage message);
    }
}
