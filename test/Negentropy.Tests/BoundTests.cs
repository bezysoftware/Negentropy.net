using FluentAssertions;

namespace Negentropy.Tests
{
    public class BoundTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1000, 1000, 0)]
        [InlineData(1, 2, -1)]
        [InlineData(2, 1, 1)]
        public void CompareBoundsByTimestamp(long t1, long t2, int expectation)
        {
            var b1 = new Bound(t1);
            var b2 = new Bound(t2);

            b1.CompareTo(b2).Should().Be(expectation);
        }

        [Theory]
        [InlineData("0000", "0000", 0)]
        [InlineData("0000", "0010", -1)]
        [InlineData("0100", "0000", 1)]
        [InlineData("1111", "111100", -1)]
        [InlineData("111100", "1111", 1)]
        public void CompareBoundsById(string x, string y, int expectation)
        {
            var b1 = new Bound(Convert.FromHexString(x), 0);
            var b2 = new Bound(Convert.FromHexString(y), 0);

            b1.CompareTo(b2).Should().Be(expectation);
        }
    }
}
