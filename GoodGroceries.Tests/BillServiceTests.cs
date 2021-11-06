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

        [Fact]
        public void GetTotalBeforeOffers_WithProducts_ReturnsTotal()
        {
            var billService = new BillService();
            billService.UpdateBasketItemQuantity(Fixtures.Products[0], 2);

            var expected = 3.0M;
            var actual = billService.GetTotalBeforeOffers();

            Assert.Equal(expected, actual);
        }
    }
}