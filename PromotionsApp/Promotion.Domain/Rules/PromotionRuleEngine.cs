using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
   public class PromotionRuleEngine : IPromotionsRules
    {
        public List<IRule> _rules { get; set; }
        public PromotionRuleEngine()
        {

        }
        
        public void AttachRules( IEnumerable< IRule> rules)
        {
            _rules.AddRange(rules);
        }
        public Task<int> ApplyPromotions(CheckOutCartDto cartDto)
        {
            int price = 1;
           if(cartDto.CheckOutCart.Count <1)
            {
                throw new Exception("Cart is Empty. Cannot Process");
            }
            foreach (var rule in _rules)
            {
                if (rule.IsMatch(cartDto)) rule.Apply(cartDto, price);
            }
            return Task.FromResult(cartDto.TotalPrice);
        }
    }
}
