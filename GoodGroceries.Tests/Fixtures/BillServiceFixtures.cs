using System.Collections.Generic;

using GoodGroceries.Models;

namespace GoodGroceries.Tests.Fixtures
{
    public class BillServiceFixtures
    {
        public List<Product> Products { get; set; }
        public List<SpecialOffer> SpecialOffers { get; set; }

        public BillServiceFixtures()
        {
            SeedFixtures();
        }

        private void SeedFixtures()
        {
            var product1 = new Product { Name = "Product 1", Price = 1.50M };
            var product2 = new Product { Name = "Product 2", Price = 3.20M };
            var product3 = new Product { Name = "Product 3", Price = 0.90M };
            this.Products = new List<Product> { product1, product2, product3 };

            var specialOffer1 = new SpecialOffer
            {
                Description = "Buy one get one free on product 1",
                RequiredProduct = product1,
                RequiredQuantity = 2,
                DiscountMultiplier = 1,
                DiscountedProduct = product1
            };

            var specialOffer2 = new SpecialOffer
            {
                Description = "Buy product 1 and get product 2 half price",
                RequiredProduct = product1,
                RequiredQuantity = 1,
                DiscountedProduct = product2,
                DiscountMultiplier = 0.5M,
            };

            var specialOffer3 = new SpecialOffer
            {
                Description = "75% off product 3",
                RequiredProduct = product3,
                RequiredQuantity = 1,
                DiscountMultiplier = 0.75M,
                DiscountedProduct = product3
            };

            this.SpecialOffers = new List<SpecialOffer> { specialOffer1, specialOffer2, specialOffer3 };
        }
    }
}