

using Checkout.Core.Extensions;

namespace Checkout.Model
{
    public struct HungarianForint
    {
        private Dictionary<int, int> currency;

        public int Sum
        {
            get
            {
                var sum = 0;
                foreach(var pair in currency)
                {
                    sum += pair.Value * pair.Key;
                }
                return sum;
            }
        }

        public HungarianForint()
        {
            currency = new Dictionary<int, int>()
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

        public int GetCount(int key)
        {
            if (currency.TryGetValue(key, out var count))
            {
                return count;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void AddCount(int key, int count)
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
                throw new ArgumentOutOfRangeException();
            }
        }

        public void FillUpStock(HungarianForint additionalStock)
        {
            foreach(var pair in currency)
            {
                currency[pair.Key] += additionalStock.GetCount(pair.Key);
                if(currency[pair.Key] < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public (HungarianForint? change, string errorMessage) CalculateChange(int changeSum)
        {
            if (Sum < changeSum)
            {
                return (null, "Not enough money to give back change");
            }

            var change = new HungarianForint();

            foreach(var pair in currency)
            {
                if(pair.Value > 0)
                {
                    var key = pair.Key;
                    var count = pair.Value;

                    var amount = changeSum / key;
                    change.AddCount(pair.Key, amount < count ? amount : count);
                    changeSum = changeSum - change.GetCount(key) * key;

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
