using System.ComponentModel.DataAnnotations;
using JWPaymentGateway.Domain.Common;
using JWPaymentGateway.Domain.Enums;

namespace JWPaymentGateway.Domain.Entities
{
    public class Card: AuditableEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public CardType CardType { get; set; }
        
        public Payment Payment { get; set; }
    }
}