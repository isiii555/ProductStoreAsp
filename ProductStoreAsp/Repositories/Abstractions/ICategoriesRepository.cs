using ProductStoreAsp.Data;
using ProductStoreAsp.Models;

namespace ProductStoreAsp.Repositories.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
