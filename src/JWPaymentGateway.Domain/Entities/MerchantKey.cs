using JWPaymentGateway.Domain.Common;

namespace JWPaymentGateway.Domain.Entities
{
    public class MerchantKey: AuditableEntity
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string ApiKey { get; set; }
        
        public Merchant Merchant { get; set; }
    }
}