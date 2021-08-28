using System;

namespace JWPaymentGateway.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        long UtcEpochNow { get; }
    }
}