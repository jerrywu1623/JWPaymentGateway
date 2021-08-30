namespace JWPaymentGateway.Application.Utils
{
    public static class StringUtil
    {
        public static string MaskCreditCardNumber(string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*');
        }
    }
}