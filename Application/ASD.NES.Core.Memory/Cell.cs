using System.Runtime.CompilerServices;


namespace ASD.NES.Core.Memory {

    // Performance Critical
    public sealed class Cell : IMemory<byte> {

        private byte b;

        public byte this[int bit] {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (byte)((b >> bit) & 1);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { if (value == 0) { b &= (byte)~(1 << bit); } else { b |= (byte)(1 << bit); } }
        }

        internal byte Value { get => b; set => b = value; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasBit(int bit) => this[bit] == 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Cell(byte cell) => new Cell(cell);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator byte(Cell cell) => cell.b;

        public Cell() : this(0b0) { }
        public Cell(byte value) => b = value;
    }
}