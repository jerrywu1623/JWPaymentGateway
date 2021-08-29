using System;
using JWPaymentGateway.Application.Interfaces;

namespace JWPaymentGateway.Infrastructure.Services
{
    public class DateTimeService: IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}