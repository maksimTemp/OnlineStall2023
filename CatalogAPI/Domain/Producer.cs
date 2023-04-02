using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Domain
{
    public class Producer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
