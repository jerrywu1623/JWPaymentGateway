using System;
using JWPaymentGateway.Application.Interfaces;

namespace JWPaymentGateway.Infrastructure.Services
{
    public class DateTimeService: IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
        public long UtcEpochNow => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}