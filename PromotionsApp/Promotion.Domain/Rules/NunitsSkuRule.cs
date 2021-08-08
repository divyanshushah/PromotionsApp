using PromotionsApp.Promotion.Domain.Entity;
using PromotionsApp.Promotion.Repository;
using System.Linq;

namespace PromotionsApp.Promotion.Domain.Rules
{
    public class NunitsSkuRule : IRule
    {
        private readonly IRepository _repository;

        public NunitsSkuRule(IRepository repository)
        {
            _repository = repository;
        }
        public bool IsActive => true;

        public void Apply(CheckOutCartDto skuDto)
        {
            int computedPrice = 0;
            var cartSkus = skuDto.CheckOutCart.ToList();
            var inventoryprice = _repository.GetInventoryPrice().UnitPriceDetails;
            var activepromo = _repository.GetActivePromotions().Nunitpromo.NunitPromotions;
            foreach (var sku in cartSkus)
            {
                var skuPrice = inventoryprice[sku.SkuName];
                var skupromo = activepromo.Where(x => x.Item1 == sku.SkuName)?.FirstOrDefault();
                var qty = sku.Quantity;
                if (skupromo != null)
                {
                    computedPrice = (qty % skupromo.Item2 * skuPrice) + (qty / skupromo.Item2 * skupromo.Item3);
                    sku.OfferApplied = true;
                    skuDto.TotalPrice += computedPrice;
                }
            }
        }

        public bool IsMatch(CheckOutCartDto skuDto)
        {
            bool IsMatch=false;
            var cartSkus = skuDto?.CheckOutCart.Where(x => x.Quantity > 1).Select(x => x.SkuName).ToList();
            var activepromo = _repository.GetActivePromotions().Nunitpromo.NunitPromotions;
            var promosku = activepromo.Select(x => x.Item1).ToList();
            foreach (var sku in cartSkus)
            {
                if (promosku.Contains(sku))
                {
                    skuDto?.OfferApplicableOn?.Add(sku);
                    IsMatch = true;
                }               
            }
            return IsMatch;
        }
    }
}
