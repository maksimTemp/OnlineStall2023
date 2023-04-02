using CatalogAPI.Domain;

namespace CatalogAPI.Models.Requests
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public Category? Category { get; set; }
        public Producer? Producer { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
    }
}
