namespace JWPaymentGateway.Application.Models
{
    public class TransactionDto
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}