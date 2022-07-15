using Checkout.Model;
using Checkout.ViewModels;

namespace Checkout.Service
{
    public interface IMoneyStockService
    {
        Task<HungarianForint> GetStockAsync();
        Task<bool> AddToStockAsync(HungarianForintVM currencyVM);
    }
}
