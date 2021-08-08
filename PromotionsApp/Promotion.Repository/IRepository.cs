using PromotionsApp.Promotion.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Repository
{
    public interface IRepository
    {
     public  InventoryDto GetInventoryPrice();
    }
}
