using System.Collections;
using System.Collections.Generic;

using GoodGroceries.Models;

namespace GoodGroceries.Tests.Fixtures
{
    public class GetTotalSavingsTestData : IEnumerable<object[]>
    {
        private readonly BillServiceFixtures _fixtures = new();

        public IEnumerator<object[]> GetEnumerator()
        {
            var batch1 = new List<(Product, int)>
            {
                (_fixtures.Products[0], 3), (_fixtures.Products[1], 4), (_fixtures.Products[2], 5),
            };

            var batch2 = new List<(Product, int)>
            {
                (_fixtures.Products[0], 1), (_fixtures.Products[1], 1), (_fixtures.Products[2], 1),
            };

            var batch3 = new List<(Product, int)>
            {
                (_fixtures.Products[0], 0), (_fixtures.Products[1], 10), (_fixtures.Products[2], 20),
            };

            yield return new object[] { batch1, 9.675M };
            yield return new object[] { batch2, 2.275M };
            yield return new object[] { batch3, 13.50M };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}