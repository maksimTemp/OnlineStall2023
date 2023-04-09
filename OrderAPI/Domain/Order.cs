using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using OrderAPI.Domain;

namespace OrderAPI.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }

        [DefaultValue(1)]
        public decimal TotalPrice { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
