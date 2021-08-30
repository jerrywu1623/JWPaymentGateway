using JWPaymentGateway.Domain.Common;
using JWPaymentGateway.Domain.Enums;

namespace JWPaymentGateway.Domain.Entities
{
    public class Payment: AuditableEntity
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int TransactionId { get; set; }
        public int CardId { get; set; }
        public string OrderNo { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        
        public Merchant Merchant { get; set; }
        public Card Card { get; set; }
        public Transaction Transaction { get; set; }
    }
}