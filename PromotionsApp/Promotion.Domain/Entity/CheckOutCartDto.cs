using System.Collections.Generic;

namespace PromotionsApp.Promotion.Domain.Entity
{
    /// <summary>
    /// Check Out Cart Data Transfer Object
    /// </summary>
    public class CheckOutCartDto
    {
        public CheckOutCartDto()
        {
            OfferApplicableOn = new List<char>();
        }
        /// <summary>
        /// Set of all different SKU after checkout
        /// </summary>
        public List<Sku> CheckOutCart { get; set; }
        /// <summary>
        /// Total Cart value After all Promotions
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// List of All SKU on which Offer is Applied
        /// </summary>
        public List<char> OfferApplicableOn { get; set; }
    }
    public class Sku
    {
        /// <summary>
        /// Sku Name
        /// </summary>
        public char SkuName { get; set; }
        /// <summary>
        /// Sku Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Offer Applied on this SKU
        /// </summary>
        public bool OfferApplied { get; set; }
    }
}
