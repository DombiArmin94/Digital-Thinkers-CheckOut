using Checkout.Model.Enums;
using Checkout.Model.Exceptions;

namespace Checkout.Model
{
    public abstract class BaseCurrency
    {
        protected Dictionary<double, int> currency;
        public Currencies CurrencyType { get; set; }

        public double Sum
        {
            get
            {
                double sum = 0;
                foreach (var pair in currency)
                {
                    sum += pair.Value * pair.Key;
                }
                return sum;
            }
        }
        public IEnumerable<KeyValuePair<double, int>> Currency
        {
            get
            {
                foreach (var pair in currency)
                {
                    yield return pair;
                }
            }

        }

        public int GetCount(double key)
        {
            if (currency.TryGetValue(key, out var count))
            {
                return count;
            }
            else
            {
                throw new InvalidCurrencyKeyException();
            }
        }

        public void FillUpStock(BaseCurrency additionalStock)
        {
            foreach (var pair in currency)
            {
                currency[pair.Key] += additionalStock.GetCount(pair.Key);
                if (currency[pair.Key] < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void AddCount(double key, int count)
        {
            if (currency.ContainsKey(key))
            {
                currency[key] += count;
                if (currency[key] < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new InvalidCurrencyKeyException();
            }
        }

    }
}
