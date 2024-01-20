using Microsoft.EntityFrameworkCore;
using ProductStoreAsp.Data;
using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;
using ProductStoreAsp.Repositories.Abstractions;

namespace ProductStoreAsp.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _dbContext;
        public OrdersRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Products)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }
    }
}
