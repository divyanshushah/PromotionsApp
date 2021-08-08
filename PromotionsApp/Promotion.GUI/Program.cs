using PromotionsApp.Promotion.Core.Entity;
using PromotionsApp.Promotion.Core.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.GUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("My Promotion App");
            var promotionEngine = new PromotionRuleEngine();
            var cartDto = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>()
            { new Sku {  SkuName = 'A', Quantity =1}  },
                TotalPrice = 1
            };
            var totalAmount = promotionEngine.ApplyPromotions(cartDto);
            Console.ReadLine();
        }
    }
}
