using ProductStoreAsp.Models;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IAppUsersRepository
    {
        Task AddProductToCart(Product product,string userId);
    }
}
