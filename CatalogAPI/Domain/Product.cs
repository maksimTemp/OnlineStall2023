using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Domain
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category? Category { get; set; }
        public Producer? Producer { get; set; }

        [DefaultValue(1)]
        public decimal Price { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }
    }
}
