using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Domain;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _service;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _service = orderItemService;
        }

        [HttpGet("{id}")]
        public async Task<OrderItem> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _service.GetAll();
        }

        //[HttpPost]
        //public async Task<Order> Create([FromBody] CreateI req)
        //{
        //    return await _service.CreateAsync(req);
        //}

        [HttpPut]
        public async Task<OrderItem> Update([FromBody] OrderItem req)
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
