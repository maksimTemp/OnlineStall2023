using AutoMapper;
using UserAPI.DataContext;
using UserAPI.Domain;
using UserAPI.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace UserAPI.Services
{
    public class IdentitiesService : IIdentityService
    {
        private readonly UsersDataContext _dbContext;
        private readonly IMapper _mapper;

        public IdentitiesService(UsersDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Identity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Identity>> GetAll()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Identity> CreateAsync(CreateIdentityRequest order)
        {
            var toCreate = _mapper.Map<Identity>(order);
            var res = await _dbContext.Orders.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Identity> UpdateAsync(Identity order)
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
    public interface IIdentityService : IService<Identity>
    {
        Task<Identity> CreateAsync(CreateIdentityRequest order);
    }
}
