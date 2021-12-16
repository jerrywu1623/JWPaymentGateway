using System.Threading.Tasks;
using JWPaymentGateway.Application.Common.Exceptions;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Commands.CreatePayment;
using JWPaymentGateway.Application.Payments.Queries.GetPayment;
using NUnit.Framework;

namespace JWPaymentGateway.Application.IntegrationTests.Payments.Queries
{
    [TestFixture]
    [Ignore("ignore_local")]
    public class GetPaymentQueryTests: TestBase
    {
        private PaymentDto _paymentDto;
        
        [SetUp]
        public async Task SetupTest()
        {
            var createPaymentCommand = new CreatePaymentCommand
            {
                MerchantId = 1,
                OrderNumber = "OD12345678",
                CardNumber = "5555555555554444",
                CardType = "VISA",
                ExpiryDate = "05/2024",
                Cvv = "114",
                Currency = "GBP",
                Amount = 100
            };

            _paymentDto = await SendAsync(createPaymentCommand);
        }

        // [Test]
        // public async Task ShouldGetPaymentDetail()
        // {
        //     //arrange
        //     var getPaymentQuery = new GetPaymentQuery
        //     {
        //         MerchantId = 1,
        //         PaymentId = _paymentDto.PaymentId
        //     };
        //     
        //     //act
        //     var result = await SendAsync(getPaymentQuery);
        //     
        //     //assert
        //     Assert.IsNotNull(result);
        // }
        //
        // [Test]
        // public void ShouldThrowNotFoundException()
        // {
        //     //arrange
        //     var getPaymentQuery = new GetPaymentQuery
        //     {
        //         MerchantId = 1,
        //         PaymentId = 2
        //     };
        //     
        //     //act & assert
        //     Assert.ThrowsAsync<NotFoundException>(() => SendAsync(getPaymentQuery));
        // }
    }
}