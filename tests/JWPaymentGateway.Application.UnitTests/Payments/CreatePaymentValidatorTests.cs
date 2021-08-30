using System;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Application.Payments.Commands.CreatePayment;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JWPaymentGateway.Application.UnitTests.Payments
{
    [TestFixture]
    public class CreatePaymentValidatorTests
    {
        private IDateTimeService _dateTimeService;
        private ILogger<CreatePaymentCommandValidator> _logger;

        [SetUp]
        public void Setup()
        {
            _dateTimeService = Substitute.For<IDateTimeService>();
            _logger = Substitute.For<ILogger<CreatePaymentCommandValidator>>();

            _dateTimeService.Now.Returns(new DateTime(2021, 8, 31));
        }
        
        [Test]
        public void ShouldBeValid()
        {
            //arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                CardType = "VISA",
                CardNumber = "5555555555554444",
                Cvv = "123",
                ExpiryDate = "05/2024",
                Currency = "GBP",
                Amount = 100,
                OrderNumber = "TEST1234"
            };
            var validator = new CreatePaymentCommandValidator(_dateTimeService, _logger);
            
            //act
            var result = validator.Validate(createPaymentCommand);
            
            //assert
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void InvalidCardNumber()
        {
            //arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                CardType = "VISA",
                CardNumber = "abctest5555554444",
                Cvv = "123",
                ExpiryDate = "05/2024",
                Currency = "GBP",
                Amount = 100,
                OrderNumber = "TEST1234"
            };
            var validator = new CreatePaymentCommandValidator(_dateTimeService, _logger);
            
            //act
            var result = validator.Validate(createPaymentCommand);
            
            //assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void InvalidCardType()
        {
            //arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                CardType = "TEST",
                CardNumber = "5555555555554444",
                Cvv = "123",
                ExpiryDate = "05/2024",
                Currency = "GBP",
                Amount = 100,
                OrderNumber = "TEST1234"
            };
            var validator = new CreatePaymentCommandValidator(_dateTimeService, _logger);
            
            //act
            var result = validator.Validate(createPaymentCommand);
            
            //assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void InvalidExpiryDate()
        {
            //arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                CardType = "VISA",
                CardNumber = "5555555555554444",
                Cvv = "123",
                ExpiryDate = "05/2021",
                Currency = "GBP",
                Amount = 100,
                OrderNumber = "TEST1234"
            };
            var validator = new CreatePaymentCommandValidator(_dateTimeService, _logger);
            
            //act
            var result = validator.Validate(createPaymentCommand);
            
            //assert
            Assert.IsFalse(result.IsValid);
        }
    }
}