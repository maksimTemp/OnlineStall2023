using CatalogAPI.Domain;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService categoryService)
        {
            _service = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Category> Create([FromBody] Category req)
        {
            return await _service.CreateAsync(req);
        }

        [HttpPut("{id}")]
        public async Task<Category> Update([FromBody] Category req)
        {
            return await _service.UpdateAsync(req);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(Guid id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
