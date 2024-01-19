using ProductStoreAsp.Models.Entities;

namespace ProductStoreAsp.Models
{
    public class Order : BaseEntity
    {
        public List<Product> Products { get; set; } = null!;
    }
}
