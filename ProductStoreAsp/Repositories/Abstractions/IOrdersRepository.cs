using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IOrdersRepository
    {
        Task AddOrderAsync(AddOrderViewModel orderViewModel);
    }
}
