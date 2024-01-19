using ProductStoreAsp.Models.Entities;

namespace ProductStoreAsp.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public List<Product>? Products { get; set;}
    }
}