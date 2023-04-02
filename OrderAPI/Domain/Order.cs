using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using OrderAPI.Domain;

namespace OrderAPI.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Status{ get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }
        public string? Adress { get; set; }

        [DefaultValue(1)]
        public decimal Price { get; set; }

        public Guid CourierId { get; set; }
        public string CourierName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
