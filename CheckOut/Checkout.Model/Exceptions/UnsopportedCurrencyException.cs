namespace Checkout.Model.Exceptions
{
    public class UnsopportedCurrencyException : Exception
    {
        public UnsopportedCurrencyException() { }

        public UnsopportedCurrencyException(string message) : base(message)
        {
        }

        public UnsopportedCurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
