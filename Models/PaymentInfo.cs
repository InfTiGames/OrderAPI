namespace OrderAPI.Models {

    /// <summary>
    /// Represents the payment information for processing an order.
    /// </summary>
    public class PaymentInfo {
        public int OrderId { get; set; }
        public bool IsPaid { get; set; }
    }
}