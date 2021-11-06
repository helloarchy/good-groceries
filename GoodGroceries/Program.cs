using System;
using System.Collections.Generic;
using GoodGroceries.Models;

namespace GoodGroceries
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Initialise products */
            var (products, specialOffers) = SeedProductsAndOffers();
        }
        

        private static (IEnumerable<Product> products, IEnumerable<SpecialOffer> specialOffers) SeedProductsAndOffers()
        {
            var bread = new Product()
            {
                Name = "Bread",
                Price = 1.10M
            };

            var milk = new Product()
            {
                Name = "Milk",
                Price = 0.50M
            };

            var cheese = new Product()
            {
                Name = "Cheese",
                Price = 0.90M
            };

            var soup = new Product()
            {
                Name = "Soup",
                Price = 0.60M
            };
            
            
            var cheeseBOGOF = new SpecialOffer()
            {
                Description = "When you buy a Cheese, you get a second Cheese free!",
                RequiredProduct = products.Find(product => string.Equals(product.Name, "Cheese", StringComparison.OrdinalIgnoreCase))
            };
            

            return new List<Product>()
            {
                bread,
                milk,
                cheese,
                soup
            };
        }
    }
}