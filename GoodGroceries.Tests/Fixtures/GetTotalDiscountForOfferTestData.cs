using System.Collections;
using System.Collections.Generic;

using GoodGroceries.Models;

namespace GoodGroceries.Tests.Fixtures
{
    public class GetTotalDiscountForOfferTestData : IEnumerable<object[]>
    {
        private readonly BillServiceFixtures _fixtures = new();
        
        public IEnumerator<object[]> GetEnumerator()
        {
            var batch1 = new List<(Product, int)>
            {
                ( _fixtures.Products[0], 3 )
            };
            
            var batch2 = new List<(Product, int)>
            {
                ( _fixtures.Products[0], 1 ), ( _fixtures.Products[1], 1 )
            };
            
            var batch3 = new List<(Product, int)>
            {
                ( _fixtures.Products[2], 4 )
            };

            yield return new object[] { batch1, _fixtures.SpecialOffers[0], 1.50M };
            yield return new object[] { batch2, _fixtures.SpecialOffers[1], 1.60M };
            yield return new object[] { batch3, _fixtures.SpecialOffers[2], 2.70M };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}