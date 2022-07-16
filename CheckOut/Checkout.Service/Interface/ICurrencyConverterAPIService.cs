using Checkout.Model.Enums;

namespace Checkout.Service
{
    public interface ICurrencyConverterAPIService
    {
        Task<float> GetCurrencyRate(Currencies from, Currencies to);
    }
}
