namespace Negentropy
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteVarInt(this BinaryWriter writer, long value)
        {
            writer.Write(VarConverter.ToVarInt(value));
        }

        public static void WriteVarInt(this BinaryWriter writer, int value)
        {
            writer.Write(VarConverter.ToVarInt((long)value));
        }

        public static void WriteId(this BinaryWriter writer, string id)
        {
            writer.Write(Convert.FromHexString(id));
        }

        public static void WriteBound(this BinaryWriter writer, Bound bound, Bound previousBound)
        {
            writer.WriteTimestamp(bound.Timestamp, previousBound.Timestamp);
            writer.WriteVarInt(bound.Id.Length);
            writer.Write(bound.Id);
        }

        public static void WriteTimestamp(this BinaryWriter writer, long timestamp, long previousTimestamp)
        {
            if (timestamp == long.MaxValue)
            {
                writer.WriteVarInt(0);
            }
            else
            {
                writer.WriteVarInt(timestamp - previousTimestamp + 1);
            }
        }

        public static string ToHexString(this BinaryWriter writer)
        {
            var stream = (MemoryStream)writer.BaseStream;

            stream.Seek(0, SeekOrigin.Begin);

            return Convert.ToHexString(stream.ToArray());
        }
    }
}