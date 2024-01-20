using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetProductsByCategory(int categoryId);

        Task RemoveProductAsync(int productId);

        Task UpdateProductAsync(int productId,ProductViewModel productViewModel);

        Task<Product> GetProductAsync(int productId);

        Task AddProductAsync(ProductViewModel productViewModel);

    }
}
