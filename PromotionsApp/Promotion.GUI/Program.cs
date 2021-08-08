using PromotionsApp.Promotion.Domain.Entity;
using PromotionsApp.Promotion.Domain.Rules;
using PromotionsApp.Promotion.Repository;
using System;
using System.Collections.Generic;

namespace PromotionsApp.Promotion.GUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("My Promotion App");
            var promotionEngine = new PromotionRuleEngine();
            promotionEngine.AttachRules(new List<IRule> { new NunitsSkuRule(new DbRepository()) ,
            new BasicRule(new DbRepository())});
            var cartDto = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>()
            { new Sku {  SkuName = 'A', Quantity =5} ,
              new Sku {  SkuName = 'B', Quantity =5},
              new Sku {  SkuName = 'C', Quantity =1} },
                TotalPrice = 0
            };
            var totalAmount = promotionEngine.ApplyPromotions(cartDto).Result;
            Console.WriteLine($"Total Price :{totalAmount}");
            Console.ReadLine();
        }
    }
}
