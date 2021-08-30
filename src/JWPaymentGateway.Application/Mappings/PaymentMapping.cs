using System;
using JWPaymentGateway.Application.Models;
using JWPaymentGateway.Application.Payments.Commands.CreatePayment;
using JWPaymentGateway.Application.Utils;
using JWPaymentGateway.Domain.Entities;
using JWPaymentGateway.Domain.Enums;
using Mapster;

namespace JWPaymentGateway.Application.Mappings
{
    public class PaymentMapping: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payment, PaymentDetail>()
                .Map(dest => dest.PaymentId, src => src.Id)
                .Map(dest => dest.OrderNumber, src => src.OrderNo)
                .Map(dest => dest.PaymentStatus, src => src.PaymentStatus)
                .Map(dest => dest.Amount, src => src.Transaction.Amount)
                .Map(dest => dest.Currency, src => src.Transaction.Currency)
                .Map(dest => dest.CardNumber, src => StringUtil.MaskCreditCardNumber(src.Card.CardNumber))
                .Map(dest => dest.ExpiryDate, src => src.Card.ExpiryDate)
                .Map(dest => dest.CardType, src => src.Card.CardType);

            config.NewConfig<CreatePaymentCommand, Payment>()
                .Map(dest => dest.OrderNo, src => src.OrderNumber);

            config.NewConfig<CreatePaymentCommand, Card>()
                .Map(dest => dest.CardType, src => Enum.Parse(typeof(CardType), src.CardType, true));

            config.NewConfig<CreatePaymentCommand, Transaction>();
        }
    }
}