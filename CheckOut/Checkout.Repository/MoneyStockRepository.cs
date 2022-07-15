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
            additionalStock.ThrowIfNull();

            FillUpStock(additionalStock);

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

        private void FillUpStock(HungarianForint additionalStock)
        {
            _stock.Five += additionalStock.Five;
            _stock.Ten += additionalStock.Ten;
            _stock.Twenty += additionalStock.Twenty;
            _stock.Fifty += additionalStock.Fifty;
            _stock.Hundred += additionalStock.Hundred;
            _stock.TwoHundred += additionalStock.TwoHundred;
            _stock.FiveHundred += additionalStock.FiveHundred;
            _stock.Thousand += additionalStock.Thousand;
            _stock.TwoThousand += additionalStock.TwoThousand;
            _stock.FiveThousand += additionalStock.FiveThousand;
            _stock.TenThousand += additionalStock.TenThousand;
            _stock.TwoThousand += additionalStock.TwoThousand;
        }
    }
}
