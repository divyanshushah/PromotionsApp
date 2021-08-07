using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
    class PromotionRuleEngine : IPromotionsRules
    {
        public List<IRule> _rules { get; set; }
        public PromotionRuleEngine()
        {

        }
        
        public void AttachRules( IEnumerable< IRule> rules)
        {
            _rules.AddRange(rules);
        }
        public int ApplyPromotions(SkuDto skuDto)
        {
            throw new NotImplementedException();
        }
    }
}
