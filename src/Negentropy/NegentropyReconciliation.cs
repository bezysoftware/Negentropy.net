namespace Negentropy
{
    /// <summary>
    /// Result of a Negentropy receonciliation.
    /// </summary>
    /// <param name="Query">Constructed query which should be passed to sever/client. Once server replies with empty string the protocol terminates.</param>
    /// <param name="HaveIds">Collection of IDs I have and the server needs.</param>
    /// <param name="NeedIds">Collection of IDS the server has and I need.</param>
    public record NegentropyReconciliation(string Query, IEnumerable<string> HaveIds,  IEnumerable<string> NeedIds)
    {
    }
}
