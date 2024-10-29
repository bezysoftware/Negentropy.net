using System.Collections;

namespace Negentropy
{
    internal record Bound(byte[] Id, long Timestamp) : IComparable<Bound>
    {
        public Bound(long Timestamp)
            : this(Array.Empty<byte>(), Timestamp)
        {
        }

        public Bound(INegentropyItem item)
            : this(Convert.FromHexString(item.Id), item.Timestamp)
        {
        }

        public static Bound Max { get; } = new Bound(long.MaxValue);
        public static Bound Min { get; } = new Bound(0);

        public int CompareTo(Bound? other)
        {
            if (other == null)
            {
                return 1;
            }

            if (Timestamp != other.Timestamp)
            {
                return Timestamp.CompareTo(other.Timestamp);
            }

            return ByteArrayComparer.Instance.Compare(Id, other.Id);
        }
    }
}
