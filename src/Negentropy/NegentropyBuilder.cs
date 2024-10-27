namespace Negentropy
{
    /// <summary>
    /// Builder used to create the <see cref="Negentropy"/> instance.
    /// </summary>
    public class NegentropyBuilder
    {
        private readonly NegentropyOptions options;
        private List<INegentropyItem> items = new();

        /// <summary>
        /// Creates a new instance of <see cref="NegentropyBuilder"/>.
        /// </summary>
        public NegentropyBuilder(NegentropyOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// Adds a single item to the collection.
        /// </summary>
        public NegentropyBuilder Add(INegentropyItem item)
        {
            this.items.Add(item);
            return this;
        }

        /// <summary>
        /// Adds a range of items to the collection.
        /// </summary>
        public NegentropyBuilder AddRange(IEnumerable<INegentropyItem> items)
        {
            this.items.AddRange(items);
            return this;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Negentropy"/>.
        /// </summary>
        /// <returns></returns>
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
