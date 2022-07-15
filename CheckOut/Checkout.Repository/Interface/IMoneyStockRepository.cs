using Checkout.Model;

namespace Checkout.Repository
{
    public interface IMoneyStockRepository
    {
        Task<HungarianForint> GetStockAsync();
        Task<bool> UpdateStock(HungarianForint additionalStock);
        Task<bool> AddToStockAsync(HungarianForint additionalStock);
    }
}
