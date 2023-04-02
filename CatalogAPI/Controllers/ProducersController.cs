using CatalogAPI.Domain;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _service;
        public ProducersController(IProducerService producerService)
        {
            _service = producerService;
        }

        [HttpGet("{id}")]
        public async Task<Producer> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Producer>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Producer> Create([FromBody] Producer req)
        {
            return await _service.CreateAsync(req);
        }

        [HttpPut("{id}")]
        public async Task<Producer> Update([FromBody] Producer req)
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
