using ProductStoreAsp.Models.ViewModels;
using ProductStoreAsp.Repositories.Abstractions;

namespace ProductStoreAsp.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public Task AddOrderAsync(AddOrderViewModel orderViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
