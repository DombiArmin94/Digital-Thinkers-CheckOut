using Checkout.Model;
using Checkout.ViewModels;

namespace Checkout.Service
{
    public interface IMoneyStockService
    {
        Task<HungarianForintVM> GetStockAsync();
        Task<bool> AddToStockAsync(HungarianForintVM currencyVM);
        Task<(HungarianForintVM change, string errorMessage)> Checkout(CheckoutVM checkoutVM);
    }
}
