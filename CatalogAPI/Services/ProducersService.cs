using CatalogAPI.DataContext;
using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services
{
    public class ProducersService : IProducerService
    {
        private readonly CatalogDataContext _dbContext;


        public ProducersService(CatalogDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Producer> GetByIdAsync(Guid id)
        {
            return await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Producer>> GetAll()
        {
            return await _dbContext.Producers.ToListAsync();
        }

        public async Task<Producer> CreateAsync(Producer producer)
        {

            var res = await _dbContext.Producers.AddAsync(producer);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Producer> UpdateAsync(Producer producer)
        {
            var upd = _dbContext.Producers.Update(producer);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<Producer> producer)
        {
            _dbContext.Producers.UpdateRange(producer);
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Producers.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Producers.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }
    public interface IProducerService : IService<Producer>
    {
        Task<Producer> CreateAsync(Producer producer);
    }
}
