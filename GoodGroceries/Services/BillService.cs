using System;
using System.Collections.Generic;
using System.Linq;

using GoodGroceries.Models;

namespace GoodGroceries.Services
{
    public class BillService
    {
        private readonly IEnumerable<BasketItem> _basket;
        private readonly IEnumerable<SpecialOffer> _specialOffers;

        public BillService(IEnumerable<BasketItem> basket, IEnumerable<SpecialOffer> specialOffers)
        {
            _basket = basket;
            _specialOffers = specialOffers;
        }

        public decimal GetTotalBeforeOffers()
        {
            return _basket.Aggregate(0M, (acc, item) => (acc + (item.Product.Price * item.Quantity)));
        }

        /// <summary>
        /// Sum the quantity of a given item
        /// </summary>
        public static decimal GetTotalForItem(BasketItem basketItem)
        {
            basketItem.TotalPrice = basketItem.Product.Price * basketItem.Quantity;
            return basketItem.TotalPrice;
        }

        /// <summary>
        /// Get the total savings for a given item by the quantity in the basket
        /// </summary>
        public decimal GetOfferSavings(SpecialOffer specialOffer)
        {
            var requiredItem = _basket.FirstOrDefault(item => 
                item.Product.Name == specialOffer.RequiredProduct.Name);
            var discountedItem = _basket.FirstOrDefault(item => 
                item.Product.Name == specialOffer.DiscountedProduct.Name);
            
            /* Apply discount only if the item is in the basket along with the product to apply the
             * discount to (not always the same), and the required quantity is met. */
            if (requiredItem != null && requiredItem.Quantity >= specialOffer.RequiredQuantity &&
                discountedItem != null && discountedItem.Quantity > 0)
            {
                // Only apply discount to the required quantity
                var quotient = Math.DivRem(requiredItem.Quantity, specialOffer.RequiredQuantity, out _);

                var savings = (quotient * discountedItem.Product.Price) / specialOffer.DiscountDivisor;
                discountedItem.PriceAfterDiscount = discountedItem.TotalPrice - savings;

                return savings;
            }

            return 0M;
        }

        public decimal GetTotalSavings()
        {
            return _basket.Aggregate(0M, (acc, item) => acc + item.TotalDiscount);
        }

        public decimal GetTotalAfterSavings()
        {
            return _basket.Aggregate(0M, (acc, item) => acc + item.PriceAfterDiscount);
        }
    }
}