namespace Negentropy
{
    /// <summary>
    /// Negentropy service. Client first needs to initiate the protocol, send the message to server and reconcile the response.
    /// </summary>
    public class Negentropy
    {
        const byte PROTOCOL_VERSION = 0x61; // Version 1
        const int BUCKETS = 16;
        const int ID_SIZE = 32;
        const int RESERVE = 200;

        private readonly Bound[] items;
        private readonly NegentropyOptions options;
        
        private bool isInitiator;

        internal Negentropy(Bound[] items, NegentropyOptions options)
        {
            this.items = items;
            this.options = options;
            
            if (options.FrameSizeLimit != 0 && options.FrameSizeLimit < 4096)
            {
                throw new ArgumentException("FrameSizeLimit too small");
            }
        }

        /// <summary>
        /// Initiate the protocol. Response should be sent to server.
        /// </summary>
        public string Initiate()
        {
            if (this.isInitiator) throw new InvalidOperationException("Already initiated");

            this.isInitiator = true;

            using var writer = new BinaryWriter(new MemoryStream());
            
            writer.Write(PROTOCOL_VERSION);
            SplitRange(writer, 0, this.items.Length, Bound.Max, Bound.Min);

            return writer.ToHexString().ToLower();
        }

        /// <summary>
        /// Runs the reconciliation process.
        /// </summary>
        public NegentropyReconciliation Reconcile(string query)
        {
            using var reader = new BinaryReader(new MemoryStream(Convert.FromHexString(query)));
            using var writer = new BinaryWriter(new MemoryStream());

            var haveIds = new List<byte[]>();
            var needIds = new List<byte[]>();

            writer.Write(PROTOCOL_VERSION);

            // version
            var version = reader.ReadByte();
            if (version < 0x60 || version > 0x6F)
            {
                throw new InvalidOperationException("invalid negentropy protocol version byte");
            }

            if (version != PROTOCOL_VERSION)
            {
                if (this.isInitiator) throw new InvalidOperationException("unsupported negentropy protocol version requested: " + (version - 0x60));
                else writer.ToHexString();
            }

            var prevBoundOut = Bound.Min;
            var prevBound = Bound.Min;
            var prevIndex = 0;
            var skip = false;
            var lastValidOffset = 0L;

            Action<Bound> writeBound = (Bound b) =>
            {
                writer.WriteBound(b, prevBoundOut);
                prevBoundOut = b;
            };

            Action doSkip = () =>
            {
                if (skip)
                {
                    skip = false;
                    writeBound(prevBound);
                    writer.WriteVarInt(Mode.Skip);
                }
            };
            
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var bound = reader.ReadBound(prevBound);
                var mode = reader.ReadVarInt();
                
                var lower = prevIndex;
                var upper = Array.BinarySearch(this.items, prevIndex, items.Length - prevIndex, bound);

                if (upper < 0)
                {
                    upper = ~upper;
                }
                
                switch (mode)
                {
                    case Mode.Skip:
                        skip = true;
                        break;
                    case Mode.Fingerprint:
                        var theirFingerprint = reader.ReadBytes(Fingerprint.SIZE);
                        var ouringerprint = Fingerprint.Calculate(this.items, lower, upper);

                        if (ByteArrayComparer.Instance.Equals(theirFingerprint, ouringerprint))
                        {
                            skip = true;
                        }
                        else
                        {
                            doSkip();
                            prevBoundOut = SplitRange(writer, lower, upper, bound, prevBoundOut);
                        }
                        break;
                    case Mode.IdList:
                        var count = reader.ReadVarInt();
                        var theirIds = new HashSet<byte[]>(ByteArrayComparer.Instance);

                        for (var i = 0L; i < count; i++)
                        {
                            var id = reader.ReadBytes(ID_SIZE);
                            if (this.isInitiator)
                            {
                                theirIds.Add(id);
                            }
                        }

                        if (this.isInitiator)
                        {
                            skip = true;

                            var ourIds = this.items
                                .Skip(lower)
                                .Take(upper - lower)
                                .Select(x => x.Id)
                                .ToHashSet(ByteArrayComparer.Instance);

                            haveIds.AddRange(ourIds.Where(our => !theirIds.Contains(our)));
                            needIds.AddRange(theirIds.Where(their => !ourIds.Contains(their)));
                        }
                        else
                        {
                            doSkip();
                            
                            var endBound = bound;
                            var takeItems = 0;
                            var idsLength = 0;

                            for (var i = lower; i < upper; i++)
                            {
                                if (this.options.FrameSizeLimit > 0 && writer.BaseStream.Position + idsLength > this.options.FrameSizeLimit - RESERVE)
                                {
                                    endBound = this.items[i];
                                    upper = i;
                                    break;
                                }

                                takeItems++;
                                idsLength += this.items[i].Id.Length;
                            }

                            var responseIds = this.items
                                .Skip(lower)
                                .Take(takeItems)
                                .Select(x => x.Id)
                                .ToList();

                            writeBound(endBound);
                            writer.WriteVarInt(Mode.IdList);
                            writer.WriteVarInt(responseIds.Count);
                            responseIds.ForEach(writer.Write);
                            lastValidOffset = writer.BaseStream.Position;
                        }

                        break;
                    default:
                        throw new InvalidOperationException("Unexpected mode");
                }

                if (this.options.FrameSizeLimit > 0 && writer.BaseStream.Position > this.options.FrameSizeLimit - RESERVE)
                {
                    var remainingFingerprint = Fingerprint.Calculate(this.items, upper, this.items.Length);
                    
                    writer.BaseStream.Seek(lastValidOffset, SeekOrigin.Begin);
                    writeBound(Bound.Max);
                    writer.WriteVarInt(Mode.Fingerprint);
                    writer.Write(remainingFingerprint);

                    break;
                }
                else
                {
                    lastValidOffset = writer.BaseStream.Position;
                }

                prevIndex = upper;
                prevBound = bound;
            }

            var done = writer.BaseStream.Position == 1 && this.isInitiator;

            return new NegentropyReconciliation(
                done ? string.Empty : writer.ToHexString().ToLower(),
                haveIds.Select(x => Convert.ToHexString(x).ToLower()).ToArray(),
                needIds.Select(x => Convert.ToHexString(x).ToLower()).ToArray());
        }

        private Bound SplitRange(BinaryWriter writer, int lower, int upper, Bound upperBound, Bound previousBound)
        {
            var elements = upper - lower;

            if (elements < BUCKETS * 2)
            {
                writer.WriteBound(upperBound, previousBound);
                writer.WriteVarInt(Mode.IdList);
                writer.WriteVarInt(elements);
                
                foreach (var item in this.items.Skip(lower).Take(elements))
                {
                    writer.Write(item.Id);
                }

                return upperBound;
            } 
            else
            {
                var itemsPerBucket = elements / BUCKETS;
                var bucketsWithExtra = elements % BUCKETS;
                var curr = lower;

                for (var i = 0; i < BUCKETS; i++)
                {
                    var bucketSize = itemsPerBucket + (i < bucketsWithExtra ? 1 : 0);
                    var fingerprint = Fingerprint.Calculate(items, curr, curr + bucketSize);
                    var nextBound = upperBound;

                    curr += bucketSize;

                    if (curr != upper)
                    {
                        var prevItem = this.items[curr - 1];
                        var currItem = this.items[curr];

                        nextBound = GetMinimalBound(prevItem, currItem);
                    }

                    writer.WriteBound(nextBound, previousBound);
                    writer.WriteVarInt(Mode.Fingerprint);
                    writer.Write(fingerprint);

                    previousBound = nextBound;
                }

                return previousBound;
            }
        }

        private Bound GetMinimalBound(Bound previous, Bound current)
        {
            if (previous.Timestamp != current.Timestamp)
            {
                return new Bound(current.Timestamp);
            }

            var shared = Enumerable
                .Range(0, Math.Min(current.Id.Length, previous.Id.Length))
                .TakeWhile(i => previous.Id[i] == current.Id[i])
                .Count() + 1;
            
            var result = new byte[shared];
            
            Array.Copy(current.Id, result, shared);

            return new Bound(result, current.Timestamp);
        }
    }
}
