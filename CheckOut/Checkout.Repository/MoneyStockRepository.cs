using Checkout.Core.Extensions;
using Checkout.Model;

namespace Checkout.Repository
{
    public class MoneyStockRepository : IMoneyStockRepository
    {
        private HungarianForint _stock;

        public MoneyStockRepository()
        {
            _stock = new HungarianForint();
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
