using Checkout.Model;
using Checkout.Model.Exceptions;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Checkout.Repository
{
    public class MoneyStockRepository : IMoneyStockRepository
    {
        private readonly object HUFStockLock = new object();
        private readonly object EURStockLock = new object();
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
                lock (HUFStockLock)
                {
                    _HUFstock.FillUpStock(additionalStock);
                }
            }
            else if (additionalStock.CurrencyType == Model.Enums.Currencies.EUR)
            {
                lock (EURStockLock)
                {
                    _eurStock.FillUpStock(additionalStock);
                }
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
            lock (HUFStockLock)
            {
                _HUFstock = additionalStock;
            }
            

            //simulating async DB calls
            await Task.Delay(1);

            return true;
        }

        public async Task<HUF> GetStockAsync()
        {
            //simulating async DB calls
            await Task.Delay(1);

            return CopyStock();
        }

        // Make sure repository returns copy isntead of reference to avoid consistency problems
        private HUF CopyStock()
        {
            var huf = new HUF();

            foreach(var pair in _HUFstock)
            {
                huf.AddCount(pair.Key, pair.Value);
            }

            return huf;
        }
    }
}
