using Microsoft.AspNetCore.Mvc;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpGet("{id}")]
        public async Task<Order> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Order> Create([FromBody] CreateOrderRequest req)
        {
            return await _service.CreateAsync(req);
        }

        [HttpPut("{id}")]
        public async Task<Order> Update([FromBody] Order req)
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
