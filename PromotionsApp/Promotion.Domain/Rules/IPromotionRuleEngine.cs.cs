using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
   public interface IPromotionsRules
    {
       Task< int> ApplyPromotions(CheckOutCartDto skuDto);

    }
}
