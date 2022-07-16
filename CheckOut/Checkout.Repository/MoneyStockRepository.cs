using Checkout.Model;
using Checkout.Model.Exceptions;
using Microsoft.Extensions.Logging;

namespace Checkout.Repository
{
    public class MoneyStockRepository : IMoneyStockRepository
    {
        private BaseCurrency _HUFstock;
        private BaseCurrency _eurStock;
        private readonly ILogger _logger;

        public MoneyStockRepository(ILogger<MoneyStockRepository> logger)
        {
            _HUFstock = new HUF();
            _eurStock = new EUR();
            _logger = logger;
        }

        public async Task<bool> AddToStockAsync(BaseCurrency additionalStock)
        {
            if (additionalStock.CurrencyType == Model.Enums.Currencies.HUF)
            {
                _HUFstock.FillUpStock(additionalStock);
            }
            else if (additionalStock.CurrencyType == Model.Enums.Currencies.EUR)
            {
                _eurStock.FillUpStock(additionalStock);
            }
            else
            {
                _logger.LogWarning("Could not add to stock!");
                throw new UnsopportedCurrencyException();
            }

            //simulating async DB calls
            await Task.Delay(1);

            return true;
        }

        public async Task<bool> UpdateStock(HUF additionalStock)
        {
            _HUFstock = additionalStock;

            //simulating async DB calls
            await Task.Delay(1);

            return true;
        }

        public async Task<HUF> GetStockAsync()
        {
            //simulating async DB calls
            await Task.Delay(1);

            return (HUF)_HUFstock;
        }
    }
}
