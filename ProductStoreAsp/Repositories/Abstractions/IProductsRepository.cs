using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface IProductsRepository
    {
        Task GetAllProductsAsync();

        Task GetProductsByCategory(int categoryId);

        Task RemoveProductAsync(int productId);

        Task UpdateProductAsync(int productId,ProductViewModel productViewModel);

        Task GetProductAsync(int productId);

        Task AddProductAsync(ProductViewModel productViewModel);

    }
}
