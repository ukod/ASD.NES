﻿namespace ASD.NES.Core.ConsoleComponents.PPUParts.Registers {

    using Shared;

    /// <summary> 0x2002 - PPU status register,
    /// This register reflects the state of various functions inside the PPU.
    /// Common name: PPUSTATUS </summary>
    internal sealed class StatusRegister {

        private readonly RefOctet r;

        /// <summary> True: more than eight sprites appear on a scanline </summary>
        public bool SpriteOverflow { get => r[5]; set => r[5] = value; }

        /// <summary> True: when a nonzero pixel of sprite 0 overlaps a nonzero background pixel <para/>
        /// Cleared at dot 1 of the pre-render line.Used for raster timing </summary>
        public bool Sprite0Hit { get => r[6]; set => r[6] = value; }

        /// <summary> Vertical blank has started (True: in vblank; False: not in vblank) <para/>
        /// Set at dot 1 of line 241 (the line *after* the post-render line); cleared after reading $2002 and at dot 1 of the pre-render line </summary>
        public bool VBlank { get => r[7]; set => r[7] = value; }

        public StatusRegister(RefOctet register) => r = register;
    }
}