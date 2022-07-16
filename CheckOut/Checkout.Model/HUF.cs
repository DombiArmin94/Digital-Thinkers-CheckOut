namespace Checkout.Model
{
    public class HUF : BaseCurrency
    {
        public HUF()
        {
            CurrencyType = Enums.Currencies.HUF;
            currency = new Dictionary<double, int>()
            {
                {20000, 0 },
                {10000, 0 },
                {5000, 0 },
                {2000, 0 },
                {1000, 0 },
                {500, 0 },
                {200, 0 },
                {100, 0 },
                {50, 0 },
                {20, 0 },
                {10, 0 },
                {5, 0 }
            };
        }

        public (HUF change, string errorMessage) CalculateChange(int changeSum)
        {
            if (Sum < changeSum)
            {
                return (null, "Not enough money to give back change");
            }

            var change = new HUF();

            foreach (var pair in currency)
            {
                if (pair.Value > 0)
                {
                    var key = pair.Key;
                    var count = pair.Value;

                    var amount = changeSum / (int)key;
                    change.AddCount(pair.Key, amount < count ? amount : count);
                    changeSum = changeSum - change.GetCount(key) * (int)key;

                    currency[key] -= change.GetCount(key);
                }
            }

            if (changeSum > 5)
            {
                return (null, "No change available!");
            }
            else if (changeSum < 5 && changeSum >= 3)
            {
                if (currency[5] == 0)
                {
                    return (null, "No change available!");
                }
                else
                {
                    currency[5] += 1;
                }
            }

            return (change, null);
        }
    }
}
