using System;

namespace JWPaymentGateway.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}