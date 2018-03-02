using System;
using System.Collections.Generic;
using System.Linq;

namespace ASD.NES.Core.Memory {

    internal enum AddressSpace { CPU, PPU }

    internal sealed class Reserve {

        #region Singleton
        private static readonly Lazy<Reserve> instance = new Lazy<Reserve>(() => new Reserve());
        public static Reserve Instance => instance.Value;
        private Reserve() { }
        #endregion

        private static readonly Cell[] cpuReserve = new Cell[1024 * 1024].Defaults();
        private static readonly Cell[] ppuReserve = new Cell[1024 * 1024].Defaults();

        internal IList<Cell> CPU => cpuReserve;
        internal IList<Cell> PPU => ppuReserve;

        public IEnumerable<Cell> GetRange(AddressSpace space, int start, int count, int times)
            => GetMemory(space).Skip(start).Take(count).FlatRepeat(times);

        public IEnumerable<Cell> GetMemory(AddressSpace space) {
            switch (space) {
                case AddressSpace.CPU: return cpuReserve;
                case AddressSpace.PPU: return ppuReserve;
                default: throw new ArgumentException($"{(int)space} isn't valid space index");
            }
        }
    }

    internal static partial class Extensions {

        public static IEnumerable<T> FlatRepeat<T>(this IEnumerable<T> sequence, int times)
            => Enumerable.Repeat(sequence, times).SelectMany(item => item);

        public static Cell[] Defaults(this Cell[] cells)
            => cells.Defaults<Cell>().ToArray();

        public static IList<T> Defaults<T>(this IList<T> items) where T : new() {
            for (var i = 0; i < items.Count; i++) {
                items[i] = new T();
            }
            return items;
        }
    }
}