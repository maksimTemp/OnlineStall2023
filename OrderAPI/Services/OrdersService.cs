using AutoMapper;
using OrderAPI.DataContext;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Services
{
    public class OrdersService : IOrderService
    {
        private readonly CatalogDataContext _dbContext;
        private readonly IMapper _mapper;

        public OrdersService(CatalogDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Producer>> GetAll()
        {
            return await _dbContext.Producers.ToListAsync();
        }

        public async Task<Producer> CreateAsync(ProducerCreateRequest producer)
        {
            var toCreate = _mapper.Map<Producer>(producer);
            var res = await _dbContext.Producers.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Producer> UpdateAsync(Producer producer)
        {
            var upd = _dbContext.Producers.Update(producer);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Producers.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }
    public interface IOrderService : IService<Producer>
    {
        Task<Producer> CreateAsync(ProducerCreateRequest producer);
    }
}
