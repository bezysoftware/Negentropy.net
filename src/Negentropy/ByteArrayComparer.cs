using System.Diagnostics.CodeAnalysis;

namespace Negentropy
{
    internal class ByteArrayComparer : IComparer<byte[]>, IEqualityComparer<byte[]>
    {
        public int Compare(byte[]? x, byte[]? y)
        {
            if (x == null || y == null) throw new InvalidOperationException();

            for (int i = 0; i < Math.Min(x.Length, y.Length); i++)
            {
                if (x[i] < y[i]) return -1;
                if (x[i] > y[i]) return 1;
            }

            if (x.Length > y.Length) return 1;
            if (x.Length < y.Length) return -1;

            return 0;
        }

        public bool Equals(byte[]? x, byte[]? y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode([DisallowNull] byte[] a)
        {
            uint b = 0;
            
            for (int i = 0; i < a.Length; i++)
            {
                b = ((b << 23) | (b >> 9)) ^ a[i];
            }

            return unchecked((int)b);
        }

        public static ByteArrayComparer Instance { get; } = new ByteArrayComparer();
    }
}
