using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using System.Data.Entity;
using CatalogAPI.Domain;

namespace CatalogAPI.Services
{
    public class CategoriesService : ICategoryService
    {
        private readonly CatalogDataContext _dbContext;


        public CategoriesService(CatalogDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> CreateAsync(Category category)
        {

            var res = await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var upd = _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<Category> category)
        {
            _dbContext.Categories.UpdateRange(category);
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var toDelete = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Categories.Remove(toDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }



    public interface ICategoryService : IService<Category>
    {
        Task<Category> CreateAsync(Category category);
    }
}
