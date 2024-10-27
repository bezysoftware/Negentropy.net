using FluentAssertions;

namespace Negentropy.Tests
{
    public class FingerprintTests
    {
        [Theory]
        [InlineData("6d74e1212fcafc8a81d090f0ffbde8b0", "eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c")]
        [InlineData("96b26d110825d4b1805fc56988a3e65d", "eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c", "39b916432333e069a4386917609215cc688eb99f06fed01aadc29b1b4b92d6f0")]
        [InlineData("b8f388e194cf016a757557a09430af38", "eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c", "39b916432333e069a4386917609215cc688eb99f06fed01aadc29b1b4b92d6f0", "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bc")]
        public void CalculateFingerprint(string expectedHash, params string[] items)
        {
            var result = Fingerprint.Calculate(items);

            Convert.ToHexString(result).Should().BeEquivalentTo(expectedHash);
        }
    }
}
