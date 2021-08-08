using PromotionsApp.Promotion.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Domain.Rules
{
    public class PromotionRuleEngine : IPromotionsRules
    {
        public List<IRule> _rules { get; set; }
        public PromotionRuleEngine()
        {

        }
        /// <summary>
        /// Add All rules to Rule Engine
        /// </summary>
        /// <param name="rules"></param>
        public void AttachRules(IEnumerable<IRule> rules)
        {
            _rules = new List<IRule>();
            foreach (var rule in rules)
            {
                if (rule.IsActive) _rules.Add(rule);
            }

        }
        /// <summary>
        /// Apply Promotion on Check Out cart to calcute price
        /// Total Price can be found in cartDto.TotalPrice
        /// </summary>
        /// <param name="cartDto"></param>
        /// <returns></returns>
        public Task<int> ApplyPromotions(CheckOutCartDto cartDto)
        {
            if (cartDto.CheckOutCart.Count < 1)
            {
                throw new Exception("Cart is Empty.Cannot Process");
            }
            foreach (var rule in _rules)
            {
                if (rule.IsActive && rule.IsMatch(cartDto)) rule.Apply(cartDto);
            }
            return Task.FromResult(cartDto.TotalPrice);
        }
    }
}
