namespace Negentropy
{
    internal class ByteArrayComparer : IComparer<byte[]>
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

        public static ByteArrayComparer Instance { get; } = new ByteArrayComparer();
    }
}
