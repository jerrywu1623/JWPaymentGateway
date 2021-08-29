using System.ComponentModel.DataAnnotations;
using JWPaymentGateway.Domain.Common;

namespace JWPaymentGateway.Domain.Entities
{
    public class Card: AuditableEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CardType { get; set; }
        
        public Payment Payment { get; set; }
    }
}