using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Data;

namespace OrderAPI.Repositories {
    public class OrderRepository : IOrderRepository {

        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order) {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync() {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId) {
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == orderId);
            return order ?? throw new InvalidOperationException($"Order with ID {orderId} not found.");
        }

        public async Task UpdateOrderAsync(Order order) {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}