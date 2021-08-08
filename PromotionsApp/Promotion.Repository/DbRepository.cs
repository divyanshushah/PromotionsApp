using PromotionsApp.Promotion.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Repository
{
    class DbRepository : IRepository
    {
        public InventoryDto GetInventoryPrice()
        {
            // Inject dbContext here to  fetch  details from DB.
            var UnitPriceMapping = new InventoryDto();
            UnitPriceMapping.UnitPriceDetails = new Dictionary<char, int>
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 }
            };
            return UnitPriceMapping;

        }
    }
}
