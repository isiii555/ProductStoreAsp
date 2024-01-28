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

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return await _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            _dbContext.Products.Remove(entity!);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateProductAsync(int productId,ProductViewModel productViewModel)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            product!.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;
            product.CategoryId = productViewModel.CategoryId;

            if (productViewModel.Image is not null)
                product.ImagePath = await UploadFileHelper.UploadFile(productViewModel.Image);
            else
                product.ImagePath = (await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId)).ImagePath;

            _dbContext.Products.Update(product);

            await _dbContext.SaveChangesAsync();
        }
    }
}
