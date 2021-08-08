using FluentAssertions;
using PromotionsApp.Promotion.Domain.Entity;
using PromotionsApp.Promotion.Domain.Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PromotionsApp.Promotion.Repository;

namespace PromotionAppTest
{

    public class ShoppingCartGenerator : IEnumerable<object[]>
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
        private Mock<IRepository> _mockDbRepo;

        public PromotionRuleEngineTest()
        {
            _mockDbRepo = new Mock<IRepository>();
            _mockDbRepo.Setup(x => x.GetInventoryPrice()).Returns(new InventoryDto
            {
                UnitPriceDetails = new Dictionary<char, int>
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 }
            }
            });
            _mockDbRepo.Setup(x => x.GetActivePromotions()).Returns(new ActivePromotionsDto
            {
                Nunitpromo = new NunitActivePromo()
                {
                    NunitPromotions = new List<Tuple<char, int, int>>()
            {
                new Tuple<char, int, int>('A',3,130),
                new Tuple<char, int, int>('B',2,45),
            },
                },
                Mixpromo = new MixActivePromo()
                {
                    MixPromotions = new List<Tuple<char, char, int>>()
            {
                new Tuple<char, char, int>('C','D',30),
            }
                }
            });
        }
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
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=1},
                    new Sku() { SkuName = 'B', Quantity = 1 }, new Sku() { SkuName = 'C', Quantity=1} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
            new BasicRule(new DbRepository())});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDto);
            res.Should().BeGreaterThan(0);

        }
        [Fact]
        public async Task Cart_Fits_ScenarioA()
        {
            CheckOutCartDto cartDtoSenarioA = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=1},
                    new Sku() { SkuName = 'B', Quantity = 1 }, new Sku() { SkuName = 'C', Quantity=1} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
            new BasicRule(_mockDbRepo.Object)});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDtoSenarioA);
            res.Should().Be(100);

        }
        [Fact]
        public async Task Cart_Fits_ScenarioB()
        {
            CheckOutCartDto cartDtoSenarioB = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=5},
                    new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'C', Quantity=1} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
            new BasicRule(_mockDbRepo.Object)});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDtoSenarioB);
            res.Should().Be(370);

        }
        [Fact]
        public async Task Cart_Fits_ScenarioC()
        {
            CheckOutCartDto cartDtoSenarioC = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=3},
                    new Sku() { SkuName = 'B', Quantity = 5 },  new Sku() { SkuName = 'C', Quantity=1} ,
                    new Sku() { SkuName = 'D', Quantity=1} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
                new MixSkuRule(_mockDbRepo.Object),
            new BasicRule(_mockDbRepo.Object)});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDtoSenarioC);
            res.Should().Be(280);

        }

        [Fact(DisplayName ="3 A, 5 B, 1 D")]
        public async Task Cart_Fits_ScenarioD()
        {
            CheckOutCartDto cartDtoSenarioC = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=3},
                    new Sku() { SkuName = 'B', Quantity = 5 }, new Sku() { SkuName = 'D', Quantity=1} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
                new MixSkuRule(_mockDbRepo.Object),
            new BasicRule(_mockDbRepo.Object)});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDtoSenarioC);
            res.Should().Be(265);

        }

        [Fact(DisplayName = "3 A, 5 B, 1 C , 2 D")]
        public async Task Cart_Fits_ScenarioE()
        {
            CheckOutCartDto cartDtoSenarioC = new CheckOutCartDto()
            {
                CheckOutCart = new List<Sku>() { new Sku() { SkuName = 'A', Quantity=3},
                    new Sku() { SkuName = 'B', Quantity = 5 },new Sku() { SkuName = 'C', Quantity=1},
                    new Sku() { SkuName = 'D', Quantity= 2} }
            };
            _promotionRuleEngine.AttachRules(new List<IRule> { new NunitsSkuRule(_mockDbRepo.Object) ,
                new MixSkuRule(_mockDbRepo.Object),
            new BasicRule(_mockDbRepo.Object)});
            var res = await _promotionRuleEngine.ApplyPromotions(cartDtoSenarioC);
            res.Should().Be(295);

        }

    }
}


