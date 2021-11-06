using System;
using System.Collections.Generic;

using GoodGroceries.Models;
using GoodGroceries.Services;

using Xunit;

namespace GoodGroceries.Tests
{
    public class BillServiceTests
    {
        private readonly BillServiceFixtures _fixtures = new();

        [Fact]
        public void UpdateBasketQuantity_NoProduct_DoesNothing()
        {
            var billService = new BillService();
            billService.UpdateBasketItemQuantity(null, 0);
            
            Assert.Empty(billService.Basket);
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
            billService.UpdateBasketItemQuantity(_fixtures.Products[0], 2);
            
            var expected = 3.0M;
            var actual = billService.GetTotalBeforeOffers();

            Assert.Equal(expected, actual);
        }
    }
}