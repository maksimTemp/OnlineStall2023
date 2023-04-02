using CatalogAPI.Domain;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace CatalogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService catalogService)
        {
            _service = catalogService;
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Product> Create([FromBody] Product req)
        {
            return await _service.CreateAsync(req);
        }

        [HttpPut("{id}")]
        public async Task<Product> Update([FromBody] Product req)
        {
            return await _service.UpdateAsync(req);
        }

        [HttpPut("{id}")]
        public async Task UpdateRange([FromBody] IEnumerable<Product> req)
        {
            await _service.UpdateRangeAsync(req);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(Guid id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
