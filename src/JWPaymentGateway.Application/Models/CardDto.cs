namespace JWPaymentGateway.Application.Models
{
    public class CardDto
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public string CardType { get; set; }
    }
}