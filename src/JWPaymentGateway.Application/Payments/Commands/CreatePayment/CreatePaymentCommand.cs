using System;
using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Events;
using JWPaymentGateway.Domain.Entities;
using JWPaymentGateway.Domain.Enums;
using Mapster;
using MediatR;

namespace JWPaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<PaymentDto>
    {
        public int MerchantId { get; set; }
        public string OrderNumber { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiryDate { get; set; }
        public string CardType { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPublisher _publisher;

        public CreatePaymentCommandHandler(IApplicationDbContext applicationDbContext, IPublisher publisher)
        {
            _applicationDbContext = applicationDbContext;
            _publisher = publisher;
        }

        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Enum.TryParse(request.CardType, true, out CardType cardType);

            var card = request.Adapt<Card>();
            card.CardType = cardType;

            var transaction = request.Adapt<Transaction>();

            var payment = new Payment
            {
                OrderNo = request.OrderNumber,
                PaymentStatus = PaymentStatus.Processing,
                Card = card,
                Transaction = transaction
            };

            _applicationDbContext.Payments.Add(payment);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var paymentDto = new PaymentDto
            {
                PaymentId = payment.Id,
                TransactionId = transaction.Id,
                PaymentStatus = payment.PaymentStatus.ToString(),
                PaymentDateTime = payment.Created
            };

            var cardDto = request.Adapt<CardDto>();
            var transactionDto = transaction.Adapt<TransactionDto>();
            _publisher.Publish(new PaymentCreatedEvent(payment.Id, cardDto, transactionDto), cancellationToken);

            return paymentDto;
        }
    }
}