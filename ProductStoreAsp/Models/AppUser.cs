using Microsoft.AspNetCore.Identity;

namespace ProductStoreAsp.Models
{
    public class AppUser : IdentityUser
    {
        public List<Order>? Orders { get; set; }

        public List<Product>? Products { get; set; }
    }
}
