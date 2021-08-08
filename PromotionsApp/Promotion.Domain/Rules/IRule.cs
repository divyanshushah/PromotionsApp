using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Domain.Rules
{
   public interface IRule
    {
        bool IsActive { get;}
        bool IsMatch(CheckOutCartDto skuDto);
        int Apply(CheckOutCartDto skuDto, int price);
    }
}
