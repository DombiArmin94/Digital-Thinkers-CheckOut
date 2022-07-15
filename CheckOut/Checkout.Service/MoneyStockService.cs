using Checkout.Core.Extensions;
using Checkout.Model;
using Checkout.Repository;
using Checkout.ViewModels;

namespace Checkout.Service
{
    public class MoneyStockService : IMoneyStockService
    {
        private readonly IMoneyStockRepository _iMoneyStockRepository;

        public MoneyStockService(IMoneyStockRepository iMoneyStockRepository)
        {
            _iMoneyStockRepository = iMoneyStockRepository;
        }

        public async Task<bool> AddToStockAsync(HungarianForintVM currencyVM)
        {
            currencyVM.ThrowIfNull();

            var model = currencyVM.GetModel();

            return await _iMoneyStockRepository.AddToStockAsync(model);
        }

        public async Task<HungarianForint> GetStockAsync()
        {
            return await _iMoneyStockRepository.GetStockAsync();
        }
    }
}
