using JWPaymentGateway.Domain.Common;

namespace JWPaymentGateway.Domain.Entities
{
    public class Transaction: AuditableEntity
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        
        public Payment Payment { get; set; }
    }
}