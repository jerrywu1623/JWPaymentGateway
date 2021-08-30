using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Common.Exceptions;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Mappings;
using JWPaymentGateway.Application.Models;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWPaymentGateway.Application.Payments.Queries.GetPayment
{
    public class GetPaymentQuery: IRequest<PaymentDetail>
    {
        public int MerchantId { get; set; }
        public int PaymentId { get; set; }
    }

    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentDetail>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetPaymentQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaymentDetail> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _applicationDbContext.Payments
                .Where(w => w.Id == request.PaymentId && w.MerchantId == request.MerchantId)
                .Include(i => i.Card)
                .Include(i => i.Transaction)
                .FirstOrDefaultAsync(cancellationToken);

            if (payment == null)
            {
                throw new NotFoundException();
            }

            var paymentDetail = _mapper.Map<PaymentDetail>(payment);
            return paymentDetail;
        }
    }
}