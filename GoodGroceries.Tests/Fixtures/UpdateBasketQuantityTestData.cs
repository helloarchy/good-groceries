using System.Collections;
using System.Collections.Generic;

namespace GoodGroceries.Tests.Fixtures
{
    public class UpdateBasketQuantityTestData : IEnumerable<object[]>
    {
        private readonly BillServiceFixtures _fixtures = new();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { _fixtures.Products[0], 3 };
            yield return new object[] { _fixtures.Products[1], 4 };
            yield return new object[] { _fixtures.Products[2], 5 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}