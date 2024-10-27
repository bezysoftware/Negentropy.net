using FluentAssertions;

namespace Negentropy.Tests
{
    public class ConverterTests
    {
        [Theory]
        [InlineData(0, "00")]
        [InlineData(1, "01")]
        [InlineData(2, "02")]
        [InlineData(127, "7F")]
        [InlineData(128, "8100")]
        [InlineData(255, "817F")]
        [InlineData(256, "8200")]
        [InlineData(65535, "83FF7F")]
        [InlineData(65536, "848000")]
        [InlineData(65537, "848001")]
        public void ToVarInt(uint n, string expected)
        {
            var result = VarConverter.ToVarInt(n);
            var hex = Convert.ToHexString(result);
            
            hex.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(127)]
        [InlineData(128)]
        [InlineData(255)]
        [InlineData(256)]
        [InlineData(65535)]
        [InlineData(65536)]
        [InlineData(65537)]
        public void FromVarInt(uint n)
        {
            var result = VarConverter.FromVarInt(VarConverter.ToVarInt(n));

            result.Should().Be(n);
        }
    }
}