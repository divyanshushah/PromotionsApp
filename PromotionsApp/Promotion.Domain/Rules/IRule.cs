﻿using PromotionsApp.Promotion.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Rules
{
   public interface IRule
    {
        bool IsMatch(CheckOutCartDto skuDto);
        int Apply(CheckOutCartDto skuDto, int price);
    }
}