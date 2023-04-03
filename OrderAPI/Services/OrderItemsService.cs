using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public OrderItemsService(OrdersDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<OrderItem>> GetByItemIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> UpdateAsync(OrderItem obj)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(DeleteOrderItemRequest deleteOrderItemRequest)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItems(ItemChangedMessage message)
        {
            throw new NotImplementedException();
        }
    }
    public interface IOrderItemService : IService<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByItemIdAsync(Guid id);
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid id);
        Task UpdateItems(ItemChangedMessage message);
    }
}
