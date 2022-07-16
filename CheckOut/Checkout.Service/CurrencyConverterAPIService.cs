using Checkout.Model.Enums;
using RestSharp;
using System.Text.Json.Serialization;

namespace Checkout.Service
{
    public class CurrencyConverterAPIService : ICurrencyConverterAPIService
    {
        private const double FEE = 0.95f;
        private const string API_KEY = "2559fbb69905423ebf140696f4fc42fc";
        public async Task<double> GetCurrencyRate(Currencies from, Currencies to)
        {
            var client = new RestClient("https://api.currencyfreaks.com/");
            var request = new RestRequest($"/latest?apikey={API_KEY}&symbols={from},{to}", Method.Get);
            var response = await client.ExecuteAsync<CurrencyData>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response?.Data?.Currencies.GetRate(from, to) ?? 0;
            }
            return 0;
        }

        private class CurrencyData
        {
            [JsonPropertyName("date")]
            public string Date { get; set; }
            [JsonPropertyName("base")]
            public string BaseCurrency { get; set; }
            [JsonPropertyName("rates")]
            public CurrencyRate Currencies { get; set; }
        }

        private class CurrencyRate
        {
            [JsonPropertyName("HUF")]
            public double HUF { get; set; }
            [JsonPropertyName("EUR")]
            public double EUR { get; set; }

            public double GetRate(Currencies from, Currencies to)
            {
                if (from == to)
                {
                    return 1;
                }

                var rate = HUF * EUR * FEE;

                if (from == Currencies.HUF)
                {
                    rate = 1 / rate;
                }

                return rate;
            }
        }
    }
}
