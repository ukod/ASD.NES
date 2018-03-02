using System.Collections.Generic;
using System.Linq;

namespace ASD.NES.Core.Memory {

    public sealed class Bank : IMemory<byte> {

        private Cell[] data;

        public byte this[int address] {
            get => data[address].Value;
            set => data[address].Value = value;
        }

        internal static Bank Create(IMapper mapper)
            => new Bank(mapper
                .Select(r => Reserve.Instance.GetRange(r.Space, r.Start, r.Count, r.Times))
                .SelectMany(c => c));

        private Bank(IEnumerable<Cell> cellSequence)
            => data = cellSequence.ToArray();
    }

    public interface IMemory<T> {
        T this[int address] { get; set; }
    }
}