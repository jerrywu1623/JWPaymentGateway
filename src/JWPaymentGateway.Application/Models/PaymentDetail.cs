
using JWPaymentGateway.Domain.Enums;

namespace JWPaymentGateway.Application.Models
{
    public class PaymentDetail
    {
        public int PaymentId { get; set; }
        public string OrderNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string ExpiryDate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}