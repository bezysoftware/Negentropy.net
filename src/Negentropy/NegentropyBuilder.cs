namespace Negentropy
{
    public class NegentropyBuilder
    {
        private readonly NegentropyOptions options;
        private List<INegentropyItem> items = new();

        public NegentropyBuilder(NegentropyOptions options)
        {
            this.options = options;
        }

        public NegentropyBuilder Add(INegentropyItem item)
        {
            this.items.Add(item);
            return this;
        }

        public NegentropyBuilder AddRange(IEnumerable<INegentropyItem> items)
        {
            this.items.AddRange(items);
            return this;
        }

        public Negentropy Build()
        {
            var sorted = this.items
                .Select(x => new Bound(x))
                .OrderBy(x => x)
                .ToArray();
            
            return new Negentropy(sorted, this.options);
        }
    }
}
