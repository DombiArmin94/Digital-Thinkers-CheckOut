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

        public async Task<HungarianForintVM> GetStockAsync()
        {
            var stock = await _iMoneyStockRepository.GetStockAsync();
            var vm = new HungarianForintVM(stock);
            return vm;
        }

        public async Task<(HungarianForintVM change, string errorMessage)> Checkout(CheckoutVM checkoutVM)
        {
            checkoutVM.ThrowIfNull();
            checkoutVM.InsertedMoney.ThrowIfNull();

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

            if(change != null)
            {
                return (new HungarianForintVM(change), errorMessage);
            }
            else
            {
                return (null, errorMessage);
            }
        }
    }
}
