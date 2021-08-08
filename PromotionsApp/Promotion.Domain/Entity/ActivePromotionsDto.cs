using System;
using System.Collections.Generic;

namespace PromotionsApp.Promotion.Domain.Entity
{
    /// <summary>
    /// Active Promotions Data Transafer Object
    /// </summary>
    public class ActivePromotionsDto
    {
        public NunitActivePromo Nunitpromo { get; set; }
        public MixActivePromo Mixpromo { get; set; }

    }
    /// <summary>
    /// Ordered in SKU Quantity Price
    /// </summary>
    public class NunitActivePromo
    {
        public List<Tuple<char, int, int>> NunitPromotions;
    }
    /// <summary>
    /// Ordered in SKU SKU Price
    /// </summary>
    public class MixActivePromo
    {
        public List<Tuple<char, char, int>> MixPromotions;
    }
}
