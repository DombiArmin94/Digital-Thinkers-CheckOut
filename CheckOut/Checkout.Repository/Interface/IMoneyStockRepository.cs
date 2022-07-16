using Checkout.Model;

namespace Checkout.Repository
{
    public interface IMoneyStockRepository
    {
        Task<HUF> GetStockAsync();
        Task<bool> UpdateStock(HUF additionalStock);
        Task<bool> AddToStockAsync(BaseCurrency additionalStock);
    }
}
