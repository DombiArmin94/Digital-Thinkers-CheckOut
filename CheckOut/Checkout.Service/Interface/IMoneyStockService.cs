using Checkout.ViewModels;

namespace Checkout.Service
{
    public interface IMoneyStockService
    {
        Task<CurrencyVM> GetStockAsync();
        Task<bool> AddToStockAsync(CurrencyVM currencyVM);
        Task<(CurrencyVM change, string errorMessage)> Checkout(CheckoutVM checkoutVM);
    }
}
