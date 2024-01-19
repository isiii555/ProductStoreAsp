using ProductStoreAsp.Models.Entities;
using System.ComponentModel;

namespace ProductStoreAsp.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Price { get; set; }

        public string ImagePath { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }

        public List<Order>? Orders { get; set; }

        public List<AppUser>? AppUsers { get; set; }
    }
}
