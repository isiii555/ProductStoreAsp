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
                .Where(o => o.IsAccepted)
                .Include(o => o.Products)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAdminAsync()
        {
            return await _dbContext.Orders
                .Where(o => o.IsAccepted == false)
                .Include(o => o.Products)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task SetOrderStatusAsync(int id, bool status)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (status)
                order!.IsAccepted = status;
            else 
                _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
