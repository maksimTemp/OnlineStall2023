using OrderAPI.Domain;

namespace OrderAPI.Models.Requests
{
    public class CreateOrderRequest
    {
        public string? Adress { get; set; }
        public decimal Price { get; set; }
        public Guid CourierId { get; set; }
        public string CourierName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
