using OrderAPI.Models;

namespace OrderAPI.Repositories {

    /// <summary>
    /// Interface for order repository to abstract database operations.
    /// </summary>
    public interface IOrderRepository {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);
    }
}