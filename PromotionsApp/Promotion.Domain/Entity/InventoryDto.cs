using System.Collections.Generic;

namespace PromotionsApp.Promotion.Domain.Entity
{
    /// <summary>
    /// SKU Price details
    /// </summary>
    public class InventoryDto
    {
        /// <summary>
        ///  List of SKU and Price mapping
        /// </summary>
        public Dictionary<char,int> UnitPriceDetails { get; set; }
    }
}
