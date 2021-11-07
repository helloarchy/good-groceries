using System.Collections.Generic;

using GoodGroceries.Models;
using GoodGroceries.Services;

namespace GoodGroceries
{
    class Program
    {
        /// <summary>
        /// Create the test products and special offers, initialise the billing service
        /// and start the interactive console session to run the user experience.
        /// </summary>
        public static void Main(string[] args)
        {
            (List<Product> products, List<SpecialOffer> specialOffers) = SeedProductsAndOffers();
            var billService = new BillService();
            var consoleService = new ConsoleService(billService, products, specialOffers);
            consoleService.GetInteractiveInput();
        }

        /// <summary>
        /// Create a collection of products and special offers to use as a test case
        /// </summary>
        /// <returns>A tuple of the products list and special offers list</returns>
        private static (List<Product> products, List<SpecialOffer> specialOffers) SeedProductsAndOffers()
        {
            var bread = new Product { Name = "Bread", Price = 1.10M };
            var milk = new Product { Name = "Milk", Price = 0.50M };
            var cheese = new Product { Name = "Cheese", Price = 0.90M };
            var soup = new Product { Name = "Soup", Price = 0.60M };
            var butter = new Product { Name = "Butter", Price = 1.20M };

            var cheeseBOGOF = new SpecialOffer
            {
                Description = "When you buy a Cheese, you get a second Cheese free!",
                RequiredProduct = cheese,
                RequiredQuantity = 2,
                DiscountMultiplier = 1,
                DiscountedProduct = cheese
            };

            var soupHalfPriceBread = new SpecialOffer
            {
                Description = "When you buy a Soup,you get a half price Bread!",
                RequiredProduct = soup,
                RequiredQuantity = 1,
                DiscountedProduct = bread,
                DiscountMultiplier = 0.5M,
            };

            var butterDiscount = new SpecialOffer
            {
                Description = "Get a third off butter!",
                RequiredProduct = butter,
                RequiredQuantity = 1,
                DiscountMultiplier = 1M / 3M, // 0.33...
                DiscountedProduct = butter
            };

            var products = new List<Product>
            {
                bread,
                milk,
                cheese,
                soup,
                butter
            };

            var specialOffers = new List<SpecialOffer> { cheeseBOGOF, soupHalfPriceBread, butterDiscount };

            return (products, specialOffers);
        }
    }
}