using Checkout.Core.Extensions;
using Checkout.Model;
using Checkout.Model.Enums;
using Checkout.Model.Exceptions;
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
            _iCurrencyConverterAPIService = iCurrencyConverterAPIService;
            _logger = logger;
        }

        public async Task<bool> AddToStockAsync(CurrencyVM currencyVM)
        {
            currencyVM.ThrowIfNull(logger: _logger);

            var model = currencyVM.GetModel();

            return await _iMoneyStockRepository.AddToStockAsync(model);
        }

        public async Task<CurrencyVM> GetStockAsync()
        {
            var stock = await _iMoneyStockRepository.GetStockAsync();
            var vm = new CurrencyVM(stock);
            return vm;
        }

        public async Task<(CurrencyVM change, string errorMessage)> Checkout(CheckoutVM checkoutVM)
        {
            checkoutVM.ThrowIfNull(logger: _logger);
            checkoutVM.InsertedMoney.ThrowIfNull(logger: _logger);

            var insertedMoney = checkoutVM.InsertedMoney.GetModel();
            var insertedSum = await GetInsertedMoneySumInHuf(insertedMoney);
            if (checkoutVM.Price > insertedSum)
            {
                return (null, "Not enough money Inserted");
            }

            await _iMoneyStockRepository.AddToStockAsync(insertedMoney);
            var stock = await _iMoneyStockRepository.GetStockAsync();

            var changeSum = insertedSum - checkoutVM.Price;
            if (changeSum == 0)
            {
                return (new CurrencyVM(), null);
            }

            var (change, errorMessage) = stock.CalculateChange(changeSum);

            await _iMoneyStockRepository.UpdateStock(stock);

            return (new CurrencyVM(change), errorMessage);
        }

        private async Task<int> GetInsertedMoneySumInHuf(BaseCurrency currency)
        {
            var sum = currency.Sum;

            if (currency.CurrencyType == Currencies.HUF)
            {
                return (int)sum;
            }
            else if (currency.CurrencyType == Currencies.EUR)
            {
                var rate = await _iCurrencyConverterAPIService.GetCurrencyRate(Currencies.EUR, Currencies.HUF);
                sum = sum * rate;

                return (int)sum;
            }
            else
            {
                throw new UnsopportedCurrencyException();
            }
        }
    }
}
