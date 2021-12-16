using System.Threading.Tasks;
using JWPaymentGateway.Application.Common.Exceptions;
using JWPaymentGateway.Application.Payments.Commands.CreatePayment;
using NUnit.Framework;

namespace JWPaymentGateway.Application.IntegrationTests.Payments.Commands
{
    [TestFixture]
    public class CreatePaymentCommandTests: TestBase
    {
        [Test]
        public async Task ShouldCreatePaymentSuccess()
        {
            //arrange
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

            //act
            var result = await SendAsync(createPaymentCommand);
            
            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowValidationException()
        {
            //arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                MerchantId = 1,
                OrderNumber = "OD12345678",
                CardNumber = "5555555555554444",
                CardType = "XXXX",
                ExpiryDate = "05/2024",
                Cvv = "114",
                Currency = "GBP",
                Amount = 100
            };
            
            //act & assert
            Assert.ThrowsAsync<ValidationException>(() => SendAsync(createPaymentCommand));
        }

        // [Test]
        // [Ignore("ignore_local")]
        // public async Task ShouldThrowResourceExistException()
        // {
        //     //arrange
        //     var createPaymentCommand = new CreatePaymentCommand
        //     {
        //         MerchantId = 1,
        //         OrderNumber = "OD12345678",
        //         CardNumber = "5555555555554444",
        //         CardType = "VISA",
        //         ExpiryDate = "05/2024",
        //         Cvv = "114",
        //         Currency = "GBP",
        //         Amount = 100
        //     };
        //     
        //     //act & assert
        //     await SendAsync(createPaymentCommand);
        //     Assert.ThrowsAsync<ResourceExistException>(() => SendAsync(createPaymentCommand));
        // }
    }
}