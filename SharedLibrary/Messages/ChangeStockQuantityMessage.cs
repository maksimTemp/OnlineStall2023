
using SharedLibrary.Enums;
using System.Runtime.Serialization;

namespace SharedLibrary.Messages
{
    public class ChangeStockQuantityMessage
    {
        public List<ProdQuaPair> ProductsQuantity { get; set; }
    }
    public class ProdQuaPair
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
