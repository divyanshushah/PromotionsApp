using FluentAssertions;
using PromotionsApp.Promotion.Core.Entity;
using PromotionsApp.Promotion.Core.Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PromotionAppTest
{

    public class TestDataGenerator : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> GetPersonFromDataGenerator()
        {
            yield return new object[]
            {
                new CheckOutCartDto()
                {
                    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity = 1 },
                        new Sku() { SkuName = 'B', Quantity = 1 }, new Sku() { SkuName = 'C', Quantity = 1 } }
                },
                new CheckOutCartDto()
                {
                    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity = 5 },
                        new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'C', Quantity = 1 } }
                }, new CheckOutCartDto()
                {
                    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity = 3 },
                        new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'D', Quantity = 1 } }
                }
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class PromotionRuleEngineTest
    {
        private PromotionRuleEngine _promotionRuleEngine = new PromotionRuleEngine();
        
        //private static CheckOutCartDto cartDtoSenarioA = new CheckOutCartDto()
        //{
        //    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=1},
        //        new Sku() { SkuName = 'B', Quantity = 1 }, new Sku() { SkuName = 'C', Quantity=1} }
        //};
        //private CheckOutCartDto cartDtoSenarioB = new CheckOutCartDto()
        //{
        //    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=5},
        //        new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'C', Quantity=1} }
        //};
        //private CheckOutCartDto cartDtoSenarioC = new CheckOutCartDto()
        //{
        //    CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=3},
        //        new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'D', Quantity=1} }
        [Fact]
        public void Cart_Is_Empty()
        {
            var cartDto = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>()
            };
            Action action = () => _promotionRuleEngine.ApplyPromotions(cartDto);
            action.Should().Throw<Exception>();

        }

        [Fact]
        public async Task Cart_Is_Not_Empty()
        {
            var cartDto = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>()
            };
            var res= await _promotionRuleEngine.ApplyPromotions(cartDto);
            res.Should().BeGreaterThan(0);

        }
    
}
}


