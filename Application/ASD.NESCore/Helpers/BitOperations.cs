﻿namespace ASD.NESCore.Helpers {

    internal static class BitOperations {

        public static short MakeInt16(byte highOctet, byte lowOctet)
            => (short)((highOctet << 8) | lowOctet);

        public static byte MakeInt8(byte highNybble, byte lowNybble)
            => (byte)((highNybble << 4) | (lowNybble & 0b0000_1111));
    }
}