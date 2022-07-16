﻿namespace Checkout.Model
{
    public class EUR : BaseCurrency
    {
        public EUR() : base()
        {
            CurrencyType = Enums.Currencies.EUR;
            currency = new Dictionary<double, int>()
            {
                {500, 0 },
                {200, 0 },
                {100, 0 },
                {50, 0 },
                {20, 0 },
                {10, 0 },
                {5, 0 },
                {2, 0 },
                {1, 0 },
                {0.5, 0 },
                {0.2, 0 },
                {0.1, 0 },
                {0.05, 0 },
                {0.02, 0 },
                {0.01, 0 }
            };
        }
    }
}