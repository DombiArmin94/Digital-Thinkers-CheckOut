using Checkout.Model;

namespace Checkout.Service
{
    public interface IMoneyStockService
    {
        Task<HungarianForint> GetStockAsync();
        Task<bool> AddToStockAsync(HungarianForint currency);
    }
}
