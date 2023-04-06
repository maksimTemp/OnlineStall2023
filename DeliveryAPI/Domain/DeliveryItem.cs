using System.ComponentModel;

namespace DeliveryAPI.Domain
{
    public class DeliveryItem
    {
        public Delivery Delivery { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
