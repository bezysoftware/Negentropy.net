namespace Negentropy
{
    internal static class BinaryReaderExtensions
    {
        public static long ReadVarInt(this BinaryReader reader)
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

        public static long ReadTimestamp(this BinaryReader reader, long previousTimestamp)
        {
            var timestamp = reader.ReadVarInt();
            
            timestamp = timestamp == 0 ? long.MaxValue : timestamp - 1;

            if (timestamp == long.MaxValue || previousTimestamp == long.MaxValue)
            {
                return long.MaxValue;
            }

            return timestamp + previousTimestamp;
        }
    }
}
