namespace OrderAPI.Models.Requests
{
    public class DeleteOrderItemRequest
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
