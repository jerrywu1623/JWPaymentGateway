using System.Threading.Tasks;
using JWPaymentGateway.Application.Models;

namespace JWPaymentGateway.Application.Interfaces
{
    public interface IBankAcquireService
    {
        string Bank { get; }
        Task<PaymentProcessResult> ProcessPayment(PaymentProcessRequest paymentProcessRequest);
    }
}