using Ardalis.GuardClauses;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ProductStoreAsp.Data;
using ProductStoreAsp.Helpers;
using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;
using ProductStoreAsp.Repositories.Abstractions;

namespace ProductStoreAsp.Repositories
{
    public class ProductsRepository : IProductsRepository
    {

        private readonly AppDbContext _dbContext;

        public ProductsRepository(AppDbContext dbContext)
        {
            _dbContext = Guard.Against.Null(dbContext);
        }

        public async Task AddProductAsync(ProductViewModel productViewModel)
        {
            var product = productViewModel.Adapt<Product>();
            var imagePath = await UploadFileHelper.UploadFile(productViewModel.Image);
            product.ImagePath = imagePath;
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public Task GetAllProductsAsync()
        {
            return _dbContext.Products.ToListAsync();
        }

        public Task GetProductAsync(int productId)
        {
            return _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public Task GetProductsByCategory(int categoryId)
        {
            return _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            _dbContext.Products.Remove(entity!);
            await _dbContext.SaveChangesAsync();

        }

        public Task UpdateProductAsync(int productId,ProductViewModel productViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
