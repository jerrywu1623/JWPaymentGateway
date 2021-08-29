using System;

namespace JWPaymentGateway.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}