using KellermanSoftware.CompareNetObjects;

using Ploeh.AutoFixture;

namespace Oracle.Example.UnitTests
{
    public abstract class UnitTest
    {
        protected UnitTest()
        {
            Fixture = new Fixture();

            Comparer = new CompareObjects();
        }

        protected IFixture Fixture { get; private set; }

        protected ICompareObjects Comparer { get; private set; }
    }
}