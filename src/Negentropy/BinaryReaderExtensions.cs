namespace Negentropy
{
    internal static class BinaryReaderExtensions
    {
        public static uint ReadVarInt(this BinaryReader reader)
        {
            return VarConverter.FromVarInt(reader.ReadByte);
        }

        public static Bound ReadBound(this BinaryReader reader, Bound previousBound)
        {
            var timestamp = reader.ReadTimestamp(previousBound.Timestamp);
            var length = reader.ReadVarInt();

            if (length > 32)
            {
                throw new InvalidOperationException("Bound key too long");
            }

            var id = reader.ReadBytes((int)length);

            return new Bound(id, timestamp);
        }

        public static uint ReadTimestamp(this BinaryReader reader, uint previousTimestamp)
        {
            var timestamp = reader.ReadVarInt();
            
            timestamp = timestamp == 0 ? uint.MaxValue : timestamp - 1;

            if (timestamp == uint.MaxValue || previousTimestamp == uint.MaxValue)
            {
                return uint.MaxValue;
            }

            return timestamp + previousTimestamp;
        }
    }
}
