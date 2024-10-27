using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Negentropy
{
    internal static class Fingerprint
    {
        public const int SIZE = 16;

        public static byte[] Calculate(IReadOnlyCollection<string> ids)
        {
            return Calculate(ids.Select(Convert.FromHexString).ToArray());
        }

        public static byte[] Calculate(IReadOnlyCollection<Bound> bounds, int lower, int upper)
        {
            return Calculate(bounds.Skip(lower).Take(upper - lower).Select(x => x.Id).ToArray());
        }

        public static byte[] Calculate(IReadOnlyCollection<byte[]> ids)
        {
            var accumulator = new byte[32];

            foreach (var bytes in ids)
            {
                var currCarry = 0;
                var nextCarry = 0;

                for (int i = 0; i < 8; i++)
                {
                    var offset = i * 4;
                    var a = BitConverter.ToUInt32(accumulator, offset);
                    var b = BitConverter.ToUInt32(bytes, offset);

                    var next = a + currCarry + b;

                    if (next > 0xFFFFFFFF)
                    {
                        nextCarry = 1;
                    }

                    var nextBytes = BitConverter.GetBytes(next & 0xFFFFFFFF);

                    Array.Copy(nextBytes, 0, accumulator, offset, 4);

                    currCarry = nextCarry;
                    nextCarry = 0;
                }
            }

            var length = VarConverter.ToVarInt(ids.Count);
            var hash = SHA256.Create().ComputeHash([..accumulator, ..length]);
            var result = new byte[SIZE];

            Array.Copy(hash, result, result.Length);

            return result;
        }
    }
}
