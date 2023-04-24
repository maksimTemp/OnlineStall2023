using OrderAPI.Domain;
using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using SharedLibrary.Enums;

namespace OrderAPI.Models.Requests
{
    public class CreateOrderRequest
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalPrice => OrderItems.Sum(oi => oi.TotalPrice);
        
        [DefaultValue(OrderStatuses.InProcess)]
        public OrderStatuses Status { get; set; }

        [DefaultValue(false)]
        public bool IsDelivery { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
