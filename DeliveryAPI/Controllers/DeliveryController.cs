using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;
using DeliveryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _service;

        public DeliveryController(IDeliveryService orderService)
        {
            _service = orderService;
        }

        [HttpGet("{id}")]
        public async Task<Delivery> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Delivery>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Delivery> Create([FromBody] CreateDeliveryRequest req)
        {
            return await _service.CreateAsync(req);
        }

        [HttpPut]
        public async Task<Delivery> Update([FromBody] Delivery req)
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
