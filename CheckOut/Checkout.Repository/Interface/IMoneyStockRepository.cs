using Checkout.Model;

namespace Checkout.Repository
{
    public interface IMoneyStockRepository
    {
        Task<HungarianForint> GetStockAsync();
        Task<bool> AddToStockAsync(HungarianForint additionalStock);
    }
}
