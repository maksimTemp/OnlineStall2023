using AutoMapper;
using OrderAPI.DataContext;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Services
{
    public class OrdersService : IOrderService
    {
        private readonly OrdersDataContext _dbContext;
        private readonly IMapper _mapper;

        public OrdersService(OrdersDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> CreateAsync(CreateOrderRequest order)
        {
            var toCreate = _mapper.Map<Order>(order);
            var res = await _dbContext.Orders.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var upd = _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Orders.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }
    public interface IOrderService : IService<Order>
    {
        Task<Order> CreateAsync(CreateOrderRequest order);
    }
}
