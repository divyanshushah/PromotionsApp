using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Domain.Entity
{
   public class InventoryDto
    {
        public Dictionary<char,int> UnitPriceDetails { get; set; }
    }
}
