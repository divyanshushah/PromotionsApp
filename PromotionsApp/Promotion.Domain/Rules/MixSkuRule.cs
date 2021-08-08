using PromotionsApp.Promotion.Domain.Entity;
using PromotionsApp.Promotion.Repository;
using System.Linq;

namespace PromotionsApp.Promotion.Domain.Rules
{
    public class MixSkuRule : IRule
    {
        private readonly IRepository _repository;

        public MixSkuRule(IRepository repository)
        {
            _repository = repository;
        }
        public bool IsActive => true;
        public void Apply(CheckOutCartDto skuDto)
        {
            int computedPrice = 0;
            var cartSkus = skuDto.CheckOutCart.ToList();
            var inventoryprice = _repository.GetInventoryPrice().UnitPriceDetails;
            var activepromo = _repository.GetActivePromotions().Mixpromo.MixPromotions;
            var promosku = activepromo.Select(x => new { x.Item1, x.Item2 , x.Item3 }).ToList();
            foreach (var promo in promosku)
            {
                foreach (var sku in cartSkus.Where(x=>x.OfferApplied==false))
                {
                    var skuPrice = inventoryprice[sku.SkuName];
                    var qty = sku.Quantity;
                    if (promo != null)
                    {                        
                        computedPrice = (qty -1) * skuPrice ;
                        sku.OfferApplied = true;
                        skuDto.TotalPrice += computedPrice;
                    }
                }
                skuDto.TotalPrice += promo.Item3;
            }
        }

        public bool IsMatch(CheckOutCartDto skuDto)
        {
            bool IsMatch = false;
            var offerAppliedOn = skuDto.OfferApplicableOn.ToList();
            var cartSkus = skuDto?.CheckOutCart.Where(x => !offerAppliedOn.Contains(x.SkuName))
                  .Select(x => x.SkuName).ToList();
            var activepromo = _repository.GetActivePromotions().Mixpromo.MixPromotions;
            var promosku = activepromo.Select(x =>  new { x.Item1, x.Item2} ).ToList();
            foreach (var promo in promosku)
            {
                if (cartSkus.Contains(promo.Item1) && cartSkus.Contains(promo.Item2))
                {
                    skuDto?.OfferApplicableOn?.Add(promo.Item1);
                    skuDto?.OfferApplicableOn?.Add(promo.Item2);                 
                    IsMatch = true;
                }
            }
            return IsMatch;
        }
    }
}
