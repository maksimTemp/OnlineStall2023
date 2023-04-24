using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SharedLibrary.Messages
{
    public class DeliveryCreateMessage : MessageBase
    {
        public decimal TotalPrice { get; set; }
        public ICollection<Item> Items { get; set; }
    }
    public class Item
    {
        public Guid? DeliveryId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
