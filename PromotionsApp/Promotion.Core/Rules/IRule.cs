using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
    interface IRule
    {
        bool IsMatch(SkuDto skuDto);
        int Apply(SkuDto skuDto, int price);
    }
}
