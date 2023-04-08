using DeliveryAPI.Domain;
using SharedLibrary.Enums;

namespace DeliveryAPI.Models.Requests
{
    public class CreateDeliveryRequest
    {
        public DeliveryStatuses Status { get; set; }
        public Guid CourierId { get; set; }
        public string CourierName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<DeliveryItem> Items { get; set; }
    }
}
