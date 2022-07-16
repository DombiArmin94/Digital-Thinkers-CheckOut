using Checkout.Core.Extensions;
using Checkout.Model;
using Microsoft.Extensions.Logging;

namespace Checkout.Repository
{
    public class MoneyStockRepository : IMoneyStockRepository
    {
        private HungarianForint _stock;
        private readonly ILogger _logger;

        public MoneyStockRepository(ILogger<MoneyStockRepository> logger)
        {
            _stock = new HungarianForint();
            _logger = logger;
        }

        public async Task<bool> AddToStockAsync(HungarianForint additionalStock)
        {
            _stock.FillUpStock(additionalStock);

            //simulating async DB calls
            await Task.Delay(1);

            return true;
        }

        public async Task<bool> UpdateStock(HungarianForint additionalStock)
        {
            _stock = additionalStock;

            //simulating async DB calls
            await Task.Delay(1);

            return true;
        }

        public async Task<HungarianForint> GetStockAsync()
        {
            //simulating async DB calls
            await Task.Delay(1);

            return _stock;
        }
    }
}
