using System;
using System.Collections.Generic;
using System.Linq;

using GoodGroceries.Models;

namespace GoodGroceries.Services
{
    public class BillService
    {
        public List<BasketItem> Basket { get; }

        public BillService()
        {
            Basket = new List<BasketItem>();
        }

        /// <summary>
        /// Get the total price across all items in the basket before any offers
        /// are applied.
        /// </summary>
        public decimal GetTotalBeforeOffers()
        {
            return Basket.Aggregate(0M, (acc, item) => (acc + item.TotalPrice));
        }

        /// <summary>
        /// Get the total savings for a given item by the quantity in the basket
        /// </summary>
        public decimal GetTotalDiscountForOffer(SpecialOffer specialOffer)
        {
            var requiredItem = Basket.FirstOrDefault(item =>
                item.Product.Name == specialOffer.RequiredProduct.Name);
            var discountedItem = Basket.FirstOrDefault(item =>
                item.Product.Name == specialOffer.DiscountedProduct.Name);

            /* Apply discount only if the item is in the basket along with the product to apply the
             * discount to (not always the same), and the required quantity is met. */
            if (requiredItem == null || requiredItem.Quantity < specialOffer.RequiredQuantity ||
                discountedItem is not { Quantity: > 0 })
            {
                return 0M;
            }

            // Only apply discount to the required quantity
            var quotient = Math.DivRem(requiredItem.Quantity, specialOffer.RequiredQuantity, out _);
            var savings = (quotient * discountedItem.Product.Price) * specialOffer.DiscountMultiplier;
            discountedItem.TotalDiscount = savings;

            return savings;
        }

        /// <summary>
        /// Get the total savings across all items in the basket
        /// </summary>
        public decimal GetTotalSavings()
        {
            return Basket.Aggregate(0M, (acc, item) => acc + item.TotalDiscount);
        }

        /// <summary>
        /// Update the basket quantity for a given product
        /// </summary>
        public void UpdateBasketItemQuantity(Product product, int quantity)
        {
            var basketItem = Basket.FirstOrDefault(basketItem => basketItem.Product.Name == product.Name);
            if (basketItem != null && basketItem.Quantity != quantity)
            {
                if (quantity != 0)
                {
                    basketItem.Quantity = quantity;
                    basketItem.TotalPrice = basketItem.Product.Price * quantity;
                }
                else
                {
                    Basket.Remove(basketItem);
                }
            }
            else if (product != null && quantity > 0)
            {
                Basket.Add(new BasketItem
                {
                    Product = product, Quantity = quantity, TotalPrice = product.Price * quantity
                });
            }
        }
    }
}