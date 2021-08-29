using System.Collections.Generic;
using JWPaymentGateway.Domain.Common;

namespace JWPaymentGateway.Domain.Entities
{
    public class Merchant: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        
        public ICollection<MerchantKey> MerchantKeys { get; set; }
    }
}