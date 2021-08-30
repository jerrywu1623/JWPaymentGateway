using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Events;
using JWPaymentGateway.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JWPaymentGateway.Application.BankAcquires.EventHandlers
{
    public class PaymentCreatedEventHandler: INotificationHandler<PaymentCreatedEvent>
    {
        private readonly IBankAcquireService _bankAcquireService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PaymentCreatedEventHandler> _logger;

        public PaymentCreatedEventHandler(IServiceScopeFactory serviceScopeFactory, IBankAcquireService bankAcquireService, ILogger<PaymentCreatedEventHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _bankAcquireService = bankAcquireService;
            _logger = logger;
        }

        public async Task Handle(PaymentCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"payment created event: {JsonSerializer.Serialize(notification)}");
            
            var paymentProcessRequest = new PaymentProcessRequest
            {
                CardNumber = notification.Card.CardNumber,
                ExpiryDate = notification.Card.ExpiryDate,
                Cvv = notification.Card.Cvv,
                Currency = notification.Transaction.Currency,
                Amount = notification.Transaction.Amount
            };

            try
            {
                var paymentProcessResult = await _bankAcquireService.ProcessPayment(paymentProcessRequest);

                using var scope = _serviceScopeFactory.CreateScope();
                var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var payment = await applicationDbContext.Payments.FirstOrDefaultAsync(w => w.Id == notification.PaymentId,
                    cancellationToken: cancellationToken);
                payment.PaymentStatus =
                    string.Equals(paymentProcessResult.Result, "success", StringComparison.OrdinalIgnoreCase)
                        ? PaymentStatus.Succeeded
                        : PaymentStatus.Failed;
                await applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "an exception has occured");
                throw;
            }
        }
    }
}