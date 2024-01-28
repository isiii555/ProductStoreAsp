using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllOrdersAdminAsync();

        Task SetOrderStatusAsync(int id,bool status);
        Task AddOrderAsync(Order order);
    }
}
