using PromotionsApp.Promotion.Domain.Entity;

namespace PromotionsApp.Promotion.Repository
{
    public interface IRepository
    {
        public InventoryDto GetInventoryPrice();
        public ActivePromotionsDto GetActivePromotions();
    }
}