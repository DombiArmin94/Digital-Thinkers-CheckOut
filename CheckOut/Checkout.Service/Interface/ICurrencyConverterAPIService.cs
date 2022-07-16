using Checkout.Model.Enums;

namespace Checkout.Service
{
    public interface ICurrencyConverterAPIService
    {
        Task<double> GetCurrencyRate(Currencies from, Currencies to);
    }
}
