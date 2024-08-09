using System.Threading.Channels;
using OrderAPI.Models;

namespace OrderAPI.Services {

    /// <summary>
    /// Background service to process payment information asynchronously.
    /// </summary>
    public class PaymentProcessingService : BackgroundService {

        private readonly Channel<PaymentInfo> _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PaymentProcessingService> _logger;

        public PaymentProcessingService(Channel<PaymentInfo> channel, IServiceScopeFactory serviceScopeFactory, ILogger<PaymentProcessingService> logger) {
            _channel = channel;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            await foreach(var paymentInfo in _channel.Reader.ReadAllAsync(stoppingToken)) {
                // Create new scope for requests
                using(var scope = _serviceScopeFactory.CreateScope()) {
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                    _logger.LogInformation($"Processing payment for Order ID: {paymentInfo.OrderId}, Paid: {paymentInfo.IsPaid}");

                    await orderService.ProcessPaymentAsync(paymentInfo);
                }
            }
        }
    }
}