using ProductStoreAsp.Models;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IAppUsersRepository
    {
        Task AddProductToCart(Product product,string userId);

        Task ClearCart(string userId);

        Task<AppUser?> GetUserByIdAsync(string userId);
    }
}
