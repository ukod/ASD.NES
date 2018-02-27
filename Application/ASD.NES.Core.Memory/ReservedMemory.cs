using System;
using System.Collections.Generic;
using System.Linq;

namespace ASD.NES.Core.Memory {

    public sealed class ReservedMemory {

        #region Singleton
        private static readonly Lazy<ReservedMemory> instance = new Lazy<ReservedMemory>(() => new ReservedMemory());
        public static ReservedMemory Instance => instance.Value;
        private ReservedMemory() { }
        #endregion

        private static readonly Cell[] cpuReserve = new Cell[1024 * 1024].Defaults();
        private static readonly Cell[] ppuReserve = new Cell[1024 * 1024].Defaults();

        //public Cell[] CPUAddressSpace =>

        private IEnumerable<Cell> GetCPUInternal() => new[] { GetZeroPage(), GetStack(), GetCPURAM() }.SelectMany(c => c);

        private IEnumerable<Cell> GetZeroPage() => cpuReserve.GetRange(0, 0x100);
        private IEnumerable<Cell> GetStack() => cpuReserve.GetRange(0x100, 0x100);
        private IEnumerable<Cell> GetCPURAM() => cpuReserve.GetRange(0x200, 0x600);

    }

    internal static partial class Extensions {

        //private static T[] T<T>(this T[] cells, int start, int count)
        //    => cells.Skip(start).Take(count).ToArray();

        public static IEnumerable<T> FlatRepeat<T>(this IEnumerable<T> sequence, int times)
            => Enumerable.Repeat(sequence, times).SelectMany(item => item);

        public static IEnumerable<T> GetRange<T>(this ICollection<T> items, int start, int count)
            => items.Skip(start).Take(count);

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