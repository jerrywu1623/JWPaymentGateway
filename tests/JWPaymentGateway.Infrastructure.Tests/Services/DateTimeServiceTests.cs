using System;
using JWPaymentGateway.Infrastructure.Services;
using NUnit.Framework;

namespace JWPaymentGateway.Infrastructure.Tests.Services
{
    [TestFixture]
    public class DateTimeServiceTests
    {
        [Test]
        public void ShouldReturnDateTimeNow()
        {
            //arrange
            var datetimeService = new DateTimeService();
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            
            //act
            var result = datetimeService.Now;
            
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(year, result.Year);
            Assert.AreEqual(month, result.Month);
            Assert.AreEqual(day, result.Day);
        }
    }
}