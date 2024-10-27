namespace Negentropy
{
    public record NegentropyReconciliation(string? Query, IEnumerable<string> HaveIds,  IEnumerable<string> NeedIds)
    {
    }
}
