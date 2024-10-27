using System.Collections;

namespace Negentropy
{
    internal record Bound(byte[] Id, uint Timestamp) : IComparable<Bound>
    {
        public Bound(uint Timestamp)
            : this(Array.Empty<byte>(), Timestamp)
        {
        }

        public Bound(INegentropyItem item)
            : this(Convert.FromHexString(item.Id), item.Timestamp)
        {
        }

        public static Bound Max { get; } = new Bound(uint.MaxValue);
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

            return StructuralComparisons.StructuralComparer.Compare(Id, other.Id);
        }
    }
}
