using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using OrderAPI.Domain;
using SharedLibrary.Enums;

namespace OrderAPI.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalPrice => OrderItems.Sum(oi => oi.TotalPrice);

        public OrderStatuses Status { get; set; }

        [DefaultValue(false)]
        public bool IsDelivery { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
