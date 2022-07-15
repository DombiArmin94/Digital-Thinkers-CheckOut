using Checkout.Core.Extensions;
using Checkout.Model;
using Checkout.Repository;

namespace Checkout.Service
{
    public class MoneyStockService : IMoneyStockService
    {
        private readonly IMoneyStockRepository _iMoneyStockRepository;

        public MoneyStockService(IMoneyStockRepository iMoneyStockRepository)
        {
            _iMoneyStockRepository = iMoneyStockRepository;
        }

        public async Task<bool> AddToStockAsync(HungarianForint currency)
        {
            currency.ThrowIfNull();

            return await _iMoneyStockRepository.AddToStockAsync(currency);
        }

        public async Task<HungarianForint> GetStockAsync()
        {
            return await _iMoneyStockRepository.GetStockAsync();
        }
    }
}
