using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Core.Entity
{
    public class CheckOutCartDto
    {
        public List<Sku> CheckOutCart { get; set; }
        public int TotalPrice { get; set; }
    }
    public class Sku
    {
        public char SkuName { get; set; }
        public int Quantity { get; set; }
    }
}
