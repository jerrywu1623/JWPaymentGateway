using System;

namespace JWPaymentGateway.Application.Common.Exceptions
{
    public class ResourceExistException: Exception
    {
        public ResourceExistException() : base()
        {
        }

        public ResourceExistException(string message) : base(message)
        {
        }
    }
}