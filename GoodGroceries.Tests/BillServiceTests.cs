using System.Collections.Generic;
using System.Runtime.CompilerServices;

using GoodGroceries.Models;
using GoodGroceries.Services;
using GoodGroceries.Tests.Fixtures;

using Xunit;

namespace GoodGroceries.Tests
{
    public class BillServiceTests
    {
        private static readonly BillServiceFixtures Fixtures = new();

        [Fact]
        public void UpdateBasketQuantity_NoProduct_DoesNothing()
        {
            var billService = new BillService();
            billService.UpdateBasketItemQuantity(null, 0);

            Assert.Empty(billService.Basket);
        }

        [Theory]
        [ClassData(typeof(UpdateBasketQuantityTestData))]
        public void UpdateBasketQuantity_ManyProducts_AddsAll(Product product, int quantity)
        {
            var billService = new BillService();
            billService.UpdateBasketItemQuantity(product, quantity);

            Assert.NotEmpty(billService.Basket);
            Assert.Contains(billService.Basket, item => item.Product == product && item.Quantity == quantity);
        }

        [Fact]
        public void GetTotalBeforeOffers_NoProducts_ReturnsZero()
        {
            var billService = new BillService();
            var expected = 0;
            var actual = billService.GetTotalBeforeOffers();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(GetTotalBeforeOffersTestData))]
        public void GetTotalBeforeOffers_WithProducts_ReturnsTotal(List<(Product, int)> productAndQuantity,
            decimal expectedTotalPrice)
        {
            var billService = new BillService();
            foreach ((Product product, int quantity) in productAndQuantity)
            {
                billService.UpdateBasketItemQuantity(product, quantity);
            }

            var actual = billService.GetTotalBeforeOffers();
            Assert.Equal(expectedTotalPrice, actual);
        }

        [Fact]
        public void GetTotalDiscountForOffer_NoProducts_ReturnsZero()
        {
            var billService = new BillService();
            var expected = 0;
            var actual = billService.GetTotalDiscountForOffer(Fixtures.SpecialOffers[0]);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(GetTotalDiscountForOfferTestData))]
        public void GetTotalDiscountForOffer_WithProducts_ReturnsDiscount(List<(Product, int)> productAndQuantity,
            SpecialOffer offer, decimal expectedTotalDiscount)
        {
            var billService = new BillService();
            foreach ((Product product, int quantity) in productAndQuantity)
            {
                billService.UpdateBasketItemQuantity(product, quantity);
            }

            var actual = billService.GetTotalDiscountForOffer(offer);
            Assert.Equal(expectedTotalDiscount, actual);
        }

        [Fact]
        public void GetTotalSavings_NoProducts_ReturnsZero()
        {
            var billService = new BillService();
            var expected = 0;
            var actual = billService.GetTotalSavings();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(GetTotalSavingsTestData))]
        public void GetTotalSavings_WithProducts_ReturnsDiscount(List<(Product, int)> productAndQuantity,
            decimal expectedTotalDiscount)
        {
            var billService = new BillService();
            foreach ((Product product, int quantity) in productAndQuantity)
            {
                billService.UpdateBasketItemQuantity(product, quantity);
            }

            // Apply savings
            foreach (SpecialOffer specialOffer in Fixtures.SpecialOffers)
            {
                billService.GetTotalDiscountForOffer(specialOffer);
            }

            var actual = billService.GetTotalSavings();
            Assert.Equal(expectedTotalDiscount, actual);
        }
    }
}