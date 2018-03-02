using System.Collections.Generic;

namespace ASD.NES.Core.Memory {

    internal struct Range : IRange {

        public AddressSpace Space { get; }

        public int Start { get; }

        public int Count { get; }

        public int Times { get; }

        internal Range(AddressSpace space, int start, int count, int times) {
            Space = space; Start = start; Count = count; Times = times;
        }
    }

    internal interface IMapper : IEnumerable<IRange> {
        void AddRange(IRange range);
    }

    internal interface IRange {
        AddressSpace Space { get; }
        int Start { get; }
        int Count { get; }
        int Times { get; }
    }
}