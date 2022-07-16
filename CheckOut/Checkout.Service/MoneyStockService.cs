using Checkout.Core.Extensions;
using Checkout.Model;
using Checkout.Repository;
using Checkout.ViewModels;
using Microsoft.Extensions.Logging;

namespace Checkout.Service
{
    public class MoneyStockService : IMoneyStockService
    {
        private readonly IMoneyStockRepository _iMoneyStockRepository;
        private readonly ICurrencyConverterAPIService _iCurrencyConverterAPIService;
        private readonly ILogger _logger;

        public MoneyStockService(IMoneyStockRepository iMoneyStockRepository, ICurrencyConverterAPIService iCurrencyConverterAPIService, ILogger<MoneyStockService> logger)
        {
            _iMoneyStockRepository = iMoneyStockRepository;
            _logger = logger;
        }

        public async Task<bool> AddToStockAsync(HungarianForintVM currencyVM)
        {
            currencyVM.ThrowIfNull(logger: _logger);

            var model = currencyVM.GetModel();

            return await _iMoneyStockRepository.AddToStockAsync(model);
        }

        public async Task<HungarianForintVM> GetStockAsync()
        {
            var stock = await _iMoneyStockRepository.GetStockAsync();
            var vm = new HungarianForintVM(stock);
            return vm;
        }

        public async Task<(HungarianForintVM change, string errorMessage)> Checkout(CheckoutVM checkoutVM)
        {
            checkoutVM.ThrowIfNull(logger: _logger);
            checkoutVM.InsertedMoney.ThrowIfNull(logger: _logger);

            var insertedMoney = checkoutVM.InsertedMoney.GetModel();
            if (checkoutVM.Price > insertedMoney.Sum)
            {
                return (null, "Not enough money Inserted");
            }

            var stock = await _iMoneyStockRepository.GetStockAsync();
            stock.FillUpStock(insertedMoney);

            var changeSum = insertedMoney.Sum - checkoutVM.Price;
            if(changeSum == 0)
            {
                return (new HungarianForintVM(), null);
            }

            var (change, errorMessage) = stock.CalculateChange(changeSum);

            await _iMoneyStockRepository.UpdateStock(stock);

            if (change != null)
            {
                return (new HungarianForintVM(change.Value), errorMessage);
            }
            else
            {
                return (null, errorMessage);
            }
        }
    }
}
