using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
    interface IPromotionsRules
    {
        int ApplyPromotions(SkuDto skuDto);

    }
}
