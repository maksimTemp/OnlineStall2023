using Microsoft.EntityFrameworkCore;
using CatalogAPI.DataContext;
using System.Data.Entity;
using CatalogAPI.Domain;
using AutoMapper;
using CatalogAPI.Models.Requests;

namespace CatalogAPI.Services
{
    public class CategoriesService : ICategoryService
    {
        private readonly CatalogDataContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriesService(CatalogDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> CreateAsync(CategoryCreateRequest category)
        {
            var toCreate = _mapper.Map<Category>(category);
            var res = await _dbContext.Categories.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var upd = _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(upd.Entity);
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
        Task<Category> CreateAsync(CategoryCreateRequest category);
    }
}
