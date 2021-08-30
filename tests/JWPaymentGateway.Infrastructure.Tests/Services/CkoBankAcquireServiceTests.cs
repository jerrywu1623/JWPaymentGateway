using System.Threading.Tasks;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Infrastructure.Services;
using NUnit.Framework;

namespace JWPaymentGateway.Infrastructure.Tests.Services
{
    [TestFixture]
    public class CkoBankAcquireServiceTests
    {
        [Test]
        public async Task ShouldProcessSuccess()
        {
            //arrange
            var ckoBankAcquireService = new CkoBankAcquireService();
            var paymentProcessRequest = new PaymentProcessRequest
            {
                CardNumber = "5555555555554444",
                ExpiryDate = "05/2024",
                Cvv = "114",
                Currency = "GBP",
                Amount = 100
            };
            
            //act
            var result = await ckoBankAcquireService.ProcessPayment(paymentProcessRequest);
            
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("success", result.Result);
        }
    }
}