using OrderAPI.Models;
using OrderAPI.Repositories;

namespace OrderAPI.Services {

    public class OrderService : IOrderService {

        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger) {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto) {
            var order = new Order {
                CustomerName = orderDto.CustomerName,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.New,
                Items = orderDto.Items.Select(i => new OrderItem {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            try {
                var createdOrder = await _orderRepository.CreateOrderAsync(order);
                _logger.LogInformation($"Order {createdOrder.OrderId} created successfully.");

                return new OrderDTO {
                    OrderId = createdOrder.OrderId,
                    CustomerName = createdOrder.CustomerName,
                    OrderDate = createdOrder.OrderDate,
                    Status = createdOrder.Status,
                    Items = createdOrder.Items.Select(i => new OrderItemDTO {
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                };
            } catch(Exception ex) {
                _logger.LogError($"An error occurred while creating order: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync() {
            try {
                var orders = await _orderRepository.GetOrdersAsync();
                _logger.LogInformation("Orders retrieved successfully.");

                return orders.Select(o => new OrderDTO {
                    OrderId = o.OrderId,
                    CustomerName = o.CustomerName,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Items = o.Items.Select(i => new OrderItemDTO {
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                });
            } catch(Exception ex) {
                _logger.LogError($"An error occurred while retrieving orders: {ex.Message}");
                throw;
            }
        }

        public async Task ProcessPaymentAsync(PaymentInfo paymentInfo) {
            try {
                var order = await _orderRepository.GetOrderByIdAsync(paymentInfo.OrderId);
                if(order != null) {
                    order.Status = paymentInfo.IsPaid ? OrderStatus.Paid : OrderStatus.Cancelled;
                    await _orderRepository.UpdateOrderAsync(order);
                    _logger.LogInformation($"Order {paymentInfo.OrderId} payment processed. Status: {order.Status}");
                } else {
                    _logger.LogWarning($"Order {paymentInfo.OrderId} not found for payment processing.");
                }
            } catch(Exception ex) {
                _logger.LogError($"An error occurred while processing payment for order {paymentInfo.OrderId}: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId) {
            try {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if(order != null) {
                    _logger.LogInformation($"Order {orderId} retrieved successfully.");
                    return new OrderDTO {
                        OrderId = order.OrderId,
                        CustomerName = order.CustomerName,
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        Items = order.Items.Select(i => new OrderItemDTO {
                            ProductName = i.ProductName,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice
                        }).ToList()
                    };
                } else {
                    _logger.LogWarning($"Order {orderId} not found.");
                    return null;
                }
            } catch(Exception ex) {
                _logger.LogError($"An error occurred while retrieving order {orderId}: {ex.Message}");
                throw;
            }
        }
    }
}