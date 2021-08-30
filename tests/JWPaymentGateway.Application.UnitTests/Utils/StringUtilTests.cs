using JWPaymentGateway.Application.Utils;
using NUnit.Framework;

namespace JWPaymentGateway.Application.UnitTests.Utils
{
    [TestFixture]
    public class StringUtilTests
    {
        [Test]
        public void ShouldMaskCardNumber()
        {
            //arrange
            var cardNumber = "1234432156789999";
            var maskCardNumber = "************9999";
            
            //act
            var result = StringUtil.MaskCreditCardNumber(cardNumber);
            
            //assert
            Assert.AreEqual(maskCardNumber, result);
        }
    }
}