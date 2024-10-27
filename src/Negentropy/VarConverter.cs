using System;

namespace Negentropy
{
    /// <summary>
    /// Varints (variable-sized unsigned integers) are represented as base-128 digits, most significant digit first, with as few digits as possible. Bit eight (the high bit) is set on each byte except the last.
    /// </summary>
    internal static class VarConverter
    {
        public static byte[] ToVarInt(uint n)
        {
            if (n == 0)
            {
                return [0];
            }

            var temp = new byte[10];
            var i = temp.Length - 1;

            while (n != 0)
            {
                temp[i--] = (byte)(n & 0x7F);
                n >>>= 7;
            }

            var result = new byte[temp.Length - i - 1];
            Array.Copy(temp, i + 1, result, 0, result.Length);
            
            for (int j = 0; j < result.Length - 1; j++)
            {
                result[j] |= 0x80;
            }

            return result;
        }

        public static uint FromVarInt(Func<byte> nextByte)
        {
            uint result = 0;

            while (true)
            {
                var b = nextByte();
                result = (result << 7) | (uint)(b & 0x7F);

                if ((b & 128) == 0)
                {
                    return result;
                }
            }
        }

        public static uint FromVarInt(byte[] span)
        {
            var i = 0;
            return FromVarInt(() => span[i++]);
        }
    }
}
