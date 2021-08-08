using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
   public class MixSkuRule : IRule
    {
        public int Apply(CheckOutCartDto skuDto, int price)
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(CheckOutCartDto skuDto)
        {
            throw new NotImplementedException();
        }
    }
}
