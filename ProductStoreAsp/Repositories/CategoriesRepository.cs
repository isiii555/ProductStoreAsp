using Microsoft.EntityFrameworkCore;
using ProductStoreAsp.Data;
using ProductStoreAsp.Models;
using ProductStoreAsp.Repositories.Abstractions;

namespace ProductStoreAsp.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoriesRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
