﻿using PromotionsApp.Promotion.Core.Entity;
using PromotionsApp.Promotion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsApp.Promotion.Domain.Rules
{
    public class NunitsSkuRule : IRule
    {
        private readonly IRepository _repository;

        public NunitsSkuRule(IRepository repository)
        {
            _repository = repository;
        }
        public bool IsActive => true;

        public int Apply(CheckOutCartDto skuDto, int price)
        {
            int computedPrice = 0;
            var cartSkus = skuDto.CheckOutCart.ToList();
            var inventoryprice = _repository.GetInventoryPrice().UnitPriceDetails;
            var activepromo = _repository.GetActivePromotions().Nunitpromo.NunitPromotions;
            foreach (var sku in cartSkus)
            {
                var skuPrice = inventoryprice[sku.SkuName];
                var skupromo = activepromo.Where(x => x.Item1 == sku.SkuName)?.FirstOrDefault();
                var qty = sku.Quantity;
                if (skupromo != null)
                {
                    computedPrice = (qty % skupromo.Item2 * skuPrice) + (qty / skupromo.Item2 * skupromo.Item3);
                    sku.OfferApplied = true;
                    skuDto.TotalPrice += computedPrice;
                }
            }
            return computedPrice;
        }

        public bool IsMatch(CheckOutCartDto skuDto)
        {
            bool IsMatch=false;
            var cartSkus = skuDto?.CheckOutCart.Where(x => x.Quantity > 1).Select(x => x.SkuName).ToList();
            var activepromo = _repository.GetActivePromotions().Nunitpromo.NunitPromotions;
            var promosku = activepromo.Select(x => x.Item1).ToList();
            foreach (var sku in cartSkus)
            {
                if (promosku.Contains(sku))
                {
                    skuDto?.OfferApplicableOn?.Add(sku);
                 //var skuitem=   skuDto?.CheckOutCart.Where(x => x.SkuName == sku).ToList();
                    IsMatch = true;
                }               
            }
            return IsMatch;
        }
    }
}
