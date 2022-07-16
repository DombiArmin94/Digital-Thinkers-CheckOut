namespace Checkout.Model.Exceptions
{
    public class InvalidCurrencyKeyException : Exception
    {
        public InvalidCurrencyKeyException() { }

        public InvalidCurrencyKeyException(string message) : base(message)
        {
        }

        public InvalidCurrencyKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
