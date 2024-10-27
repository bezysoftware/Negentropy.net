namespace Negentropy
{
    /// <summary>
    /// Represents an item which can be used with Negentropy protocol.
    /// </summary>
    public interface INegentropyItem
    {
        /// <summary>
        /// Id of the items, 32 bytes hex.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Timestamp of the item, used for sorting. Any unit.
        /// </summary>
        long Timestamp { get; }
    }
}
