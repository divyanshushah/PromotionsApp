
using PromotionsApp.Promotion.Domain.Entity;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Domain.Rules
{
    public interface IPromotionsRules
    {
       Task< int> ApplyPromotions(CheckOutCartDto skuDto);

    }
}
