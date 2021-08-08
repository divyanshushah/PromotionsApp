
using PromotionsApp.Promotion.Domain.Entity;
using PromotionsApp.Promotion.Repository;
using System.Linq;

namespace PromotionsApp.Promotion.Domain.Rules
{
    /// <summary>
    /// Basic Rule to be applied as Last rule.
    /// It checks if no prootion is applied them caculated cost as per standard rate.
    /// </summary>
    public  class BasicRule : IRule
    {
        private readonly IRepository _repository;
        public bool IsActive => true;
        public BasicRule(IRepository repository)
        {
            _repository = repository;
        }
        public void Apply(CheckOutCartDto skuDto)
        {
            int computedPrice = 0;
            var offerAppliedOn = skuDto.OfferApplicableOn.ToList();
            var cartSku = skuDto?.CheckOutCart.Where(x => !offerAppliedOn.Contains(x.SkuName))
                  .Select(x => x).ToList();
            var inventoryprice = _repository.GetInventoryPrice().UnitPriceDetails;
            foreach (var sku in cartSku)
            {
                var skuPrice = inventoryprice[sku.SkuName];
                var qty = sku.Quantity;
                computedPrice = qty * skuPrice;
                skuDto.TotalPrice += computedPrice;
            }                
        }
        /// <summary>
        /// Checks if Basic Rule is applicable on Cart
        /// </summary>
        /// <param name="skuDto"></param>
        /// <returns></returns>
    public bool IsMatch(CheckOutCartDto skuDto)
    {
        bool IsMatch = false;
        var offerAppliedOn = skuDto.OfferApplicableOn.ToList();
        var cartSkusWhereOfferNotApplied = skuDto?.CheckOutCart.Where(x => !offerAppliedOn.Contains(x.SkuName))
              .Select(x => x.SkuName).ToList();
        foreach (var sku in cartSkusWhereOfferNotApplied)
        {
            IsMatch = true;
        }
        return IsMatch;
    }
}
}
