namespace JWPaymentGateway.Application.Models
{
    public class PaymentProcessRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}