using System;
using FluentValidation;
using JWPaymentGateway.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace JWPaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator: AbstractValidator<CreatePaymentCommand>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<CreatePaymentCommandValidator> _logger;
        public CreatePaymentCommandValidator(IDateTimeService dateTimeService, ILogger<CreatePaymentCommandValidator> logger)
        {
            _dateTimeService = dateTimeService;
            _logger = logger;

            RuleFor(x => x.CardNumber).CreditCard();
            RuleFor(x => x.OrderNumber).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .Matches("(0[1-9]|10|11|12)/20[0-9]{2}$")
                .Must(IsValidExpiryDate);
            RuleFor(x => x.Cvv)
                .MaximumLength(4)
                .NotEmpty();
            RuleFor(x => x.CardType).NotEmpty();
        }

        private bool IsValidExpiryDate(string expiryDate)
        {
            try
            {
                var dateParts = expiryDate.Split('/');
                var month = int.Parse(dateParts[0]);
                var year = int.Parse(dateParts[1]);
                var daysInMonth = DateTime.DaysInMonth(year, month);
                var cardExpiryDate = new DateTime(year, month, daysInMonth, 23, 59, 59);
                return cardExpiryDate > _dateTimeService.Now;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "an exceptions has occured");
                return false;
            }
        }
    }
}