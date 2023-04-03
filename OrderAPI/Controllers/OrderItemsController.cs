using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderItemsController(IOrderService orderService)
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

        [HttpPut]
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
