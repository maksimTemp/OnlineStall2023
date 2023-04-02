using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Domain
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
