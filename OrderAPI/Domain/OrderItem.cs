using System.ComponentModel;

namespace OrderAPI.Domain
{
    public class OrderItem
    {
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
