using System;
using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Common.Exceptions;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Events;
using JWPaymentGateway.Domain.Entities;
using JWPaymentGateway.Domain.Enums;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IApplicationDbContext applicationDbContext, IPublisher publisher, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _publisher = publisher;
            _mapper = mapper;
        }

        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            await ValidateIsPaymentExist(request.MerchantId, request.OrderNumber, cancellationToken);

            var card = _mapper.Map<Card>(request);
            var transaction = _mapper.Map<Transaction>(request);
            var payment = new Payment
            {
                MerchantId = request.MerchantId,
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

        private async Task ValidateIsPaymentExist(int merchantId, string orderNumber, CancellationToken cancellationToken)
        {
            if (await _applicationDbContext.Payments.AnyAsync(w => w.MerchantId == merchantId && w.OrderNo == orderNumber, cancellationToken))
            {
                throw new ResourceExistException($"payment exist, order number: {orderNumber}");
            }
        }
    }
}