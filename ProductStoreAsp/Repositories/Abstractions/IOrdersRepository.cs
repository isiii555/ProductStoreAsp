using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllOrdersAsync();

        Task AddOrderAsync(Order order);
    }
}
