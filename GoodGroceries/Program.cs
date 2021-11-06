using System;
using System.Collections.Generic;
using GoodGroceries.Models;
using GoodGroceries.Services;

namespace GoodGroceries
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise products
            (IEnumerable<Product> products, IEnumerable<SpecialOffer> specialOffers) = SeedProductsAndOffers();
            var consoleService = new ConsoleService();
                consoleService.GetInteractiveInput(products, specialOffers);
        }

        private static (IEnumerable<Product> products, IEnumerable<SpecialOffer> specialOffers) SeedProductsAndOffers()
        {
            var bread = new Product
            {
                Name = "Bread",
                Price = 1.10M
            };

            var milk = new Product
            {
                Name = "Milk",
                Price = 0.50M
            };

            var cheese = new Product
            {
                Name = "Cheese",
                Price = 0.90M
            };

            var soup = new Product
            {
                Name = "Soup",
                Price = 0.60M
            };

            var butter = new Product
            {
                Name = "Butter",
                Price = 1.20M
            };
            
            
            var cheeseBOGOF = new SpecialOffer
            {
                Description = "When you buy a Cheese, you get a second Cheese free!",
                RequiredProduct = cheese,
                RequiredQuantity = 2,
                DiscountDivisor = 1,
                DiscountedProduct = cheese
            };

            var soupHalfPriceBread = new SpecialOffer
            {
                Description = "When you buy a Soup,you get a half price Bread!",
                RequiredProduct = soup,
                RequiredQuantity = 1,
                DiscountedProduct = bread,
                DiscountDivisor = 2,
            };

            var butterDiscount = new SpecialOffer
            {
                Description = "Get a third off butter!",
                RequiredProduct = butter,
                RequiredQuantity = 1,
                DiscountDivisor = 3,
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

            var specialOffers = new List<SpecialOffer>
            {
                cheeseBOGOF,
                soupHalfPriceBread,
                butterDiscount
            };

            return (products, specialOffers);
        }
    }
}