using System.Threading.Tasks;
using JWPaymentGateway.Application.Constants;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Commands.CreatePayment;
using JWPaymentGateway.Application.Payments.Queries.GetPayment;
using Microsoft.AspNetCore.Mvc;

namespace JWPaymentGateway.Web.Controllers
{
    public class PaymentsController: ApiBaseController
    {
        [HttpPost]
        public async Task<PaymentDto> Post([FromBody] CreatePaymentCommand request, [FromHeader(Name = Constants.HttpHeaders.MERCHANT_ID_HEADER_NAME)] int merchantId)
        {
            request.MerchantId = merchantId;
            return await Mediator.Send(request);
        }

        [HttpGet("{paymentId:int}")]
        public async Task<PaymentDetail> Get([FromHeader(Name = Constants.HttpHeaders.MERCHANT_ID_HEADER_NAME)] int merchantId, int paymentId)
        {
            var getPaymentQuery = new GetPaymentQuery {MerchantId = merchantId, PaymentId = paymentId};
            return await Mediator.Send(getPaymentQuery);
        }
    }
}