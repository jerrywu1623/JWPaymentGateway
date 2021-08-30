using System.Threading.Tasks;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Models;

namespace JWPaymentGateway.Infrastructure.Services
{
    public class CkoBankAcquireService: IBankAcquireService
    {
        public string Bank => "CKO";
        public async Task<PaymentProcessResult> ProcessPayment(PaymentProcessRequest paymentProcessRequest)
        {
            var paymentProcessResult = new PaymentProcessResult
            {
                ErrorCode = string.Empty,
                Result = "success"
            };

            await Task.Delay(3000);

            return await Task.FromResult(paymentProcessResult);
        }
    }
}