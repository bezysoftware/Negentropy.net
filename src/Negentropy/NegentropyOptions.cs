namespace Negentropy
{
    /// <summary>
    /// Options for <see cref="Negentropy"/>.
    /// </summary>
    public record NegentropyOptions
    {
        /// <summary>
        /// Optional parameter to break large messages into small frames.
        /// </summary>
        public uint FrameSizeLimit { get; init; }
    }
}
