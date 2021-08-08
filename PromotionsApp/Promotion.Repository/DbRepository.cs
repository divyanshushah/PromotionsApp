using PromotionsApp.Promotion.Domain.Entity;
using System;
using System.Collections.Generic;

namespace PromotionsApp.Promotion.Repository
{
  public  class DbRepository : IRepository
    {
        public ActivePromotionsDto GetActivePromotions()
        {
            // Inject dbContext here to  fetch  details from DB. For Demo Returing  Promotions as mentioned in Test
            var activePromo = new ActivePromotionsDto
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
            };
            return activePromo;
        }

        public InventoryDto GetInventoryPrice()
        {
            // Inject dbContext here to  fetch  details from DB.  For Demo Returing TestSetup Prices
            var UnitPriceMapping = new InventoryDto
            {
                UnitPriceDetails = new Dictionary<char, int>
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 }
            }
            };
            return UnitPriceMapping;

        }
    }
}
