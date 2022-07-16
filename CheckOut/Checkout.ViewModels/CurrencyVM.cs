using Checkout.Model;
using Checkout.Model.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Checkout.ViewModels
{
    public class CurrencyVM
    {
        [JsonPropertyName("currencies")]
        public Dictionary<double, int> Currencies { get; set; }

        [JsonPropertyName("currencyType")]
        public string CurrencyType { get; set; }

        [JsonIgnore]
        public bool IsPositive
        {
            get
            {
                foreach (var pair in Currencies)
                {
                    if (pair.Value < 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public CurrencyVM() { }

        public CurrencyVM(HUF currency)
        {
            Currencies = new Dictionary<double, int>();
            foreach (var pair in currency.Currency)
            {
                Currencies.Add(pair.Key, pair.Value);
            }
            CurrencyType = currency.CurrencyType.ToString();
        }


        public BaseCurrency GetModel()
        {
            if (CurrencyType == Model.Enums.Currencies.HUF.ToString())
            {
                return CreateModel<HUF>();
            }
            else if (CurrencyType == Model.Enums.Currencies.EUR.ToString())
            {
                return CreateModel<EUR>();
            }
            else
            {
                throw new UnsopportedCurrencyException();
            }
        }

        private T CreateModel<T>() where T : BaseCurrency, new()
        {
            var model = new T();

            foreach (var pair in Currencies)
            {
                model.AddCount(pair.Key, pair.Value);
            }

            return model;
        }
    }
}
