using JWPaymentGateway.Application.Models;
using MediatR;

namespace JWPaymentGateway.Application.Payments.Events
{
    public class PaymentCreatedEvent: INotification
    {
        public PaymentCreatedEvent(int paymentId, CardDto card, TransactionDto transaction)
        {
            PaymentId = paymentId;
            Card = card;
            Transaction = transaction;
        }
        public int PaymentId { get; }
        public CardDto Card { get; }
        public TransactionDto Transaction { get; }
    }
}