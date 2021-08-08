using PromotionsApp.Promotion.Domain.Entity;


namespace PromotionsApp.Promotion.Domain.Rules
{
   public interface IRule
    {
        bool IsActive { get;}
        bool IsMatch(CheckOutCartDto skuDto);
        void Apply(CheckOutCartDto skuDto);
    }
}
