using OrderAPI.Models;

namespace OrderAPI.Services {

    /// <summary>
    /// Interface for the order service to handle business logic.
    /// </summary>
    public interface IOrderService {
        Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto);
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task ProcessPaymentAsync(PaymentInfo paymentInfo);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
    }
}
