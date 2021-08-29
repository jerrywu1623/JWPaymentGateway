using System;

namespace JWPaymentGateway.Application.Models
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}