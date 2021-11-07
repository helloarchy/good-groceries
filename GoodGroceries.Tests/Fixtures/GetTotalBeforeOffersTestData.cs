using System.Collections;
using System.Collections.Generic;

using GoodGroceries.Models;

namespace GoodGroceries.Tests.Fixtures
{
    /// <summary>
    /// Get batches of products with their quantities to seed the bill service
    /// with baskets of varying mixtures of products. Then test that the total
    /// price returned is correct.
    /// </summary>
    public class GetTotalBeforeOffersTestData : IEnumerable<object[]>
    {
        private readonly BillServiceFixtures _fixtures = new();
        public IEnumerator<object[]> GetEnumerator()
        {
            var batch1 = new List<(Product, int)>
            {
                ( _fixtures.Products[0], 3 ), ( _fixtures.Products[1], 4 ), (_fixtures.Products[2], 5),
            };
            
            var batch2 = new List<(Product, int)>
            {
                ( _fixtures.Products[0], 1 ), ( _fixtures.Products[1], 1 ), ( _fixtures.Products[2], 1 ),
            };
            
            var batch3 = new List<(Product, int)>
            {
                ( _fixtures.Products[0], 0 ), ( _fixtures.Products[1], 10 ), ( _fixtures.Products[2], 20 ),
            };
            
            yield return new object[] { batch1, 21.80M };
            yield return new object[] { batch2, 5.60M };
            yield return new object[] { batch3, 50.00M };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}