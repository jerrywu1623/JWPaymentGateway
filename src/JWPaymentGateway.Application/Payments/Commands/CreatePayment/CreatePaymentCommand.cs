using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Models;
using MediatR;

namespace JWPaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand: IRequest<PaymentDto>
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

        public CreatePaymentCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PaymentDto());
        }
    }
}