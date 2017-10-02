﻿namespace ASD.NESCore.Helpers {

    internal static partial class ByteExtensions {

        // public

        public static byte HigBits(this byte @byte)
            => (byte)((0b1111_0000 & @byte) >> 4);

        public static byte LowBits(this byte @byte)
            => (byte)(0b0000_1111 & @byte);

        public static bool IsSetBit(this byte @byte, int bit)
            => ((@byte >> bit) & 1) == 1;

        public static byte SetBit(this byte @byte, int bit, bool value)
            => value ? WithBit(@byte, bit) : WithoutBit(@byte, bit);

        // private

        private static byte WithBit(byte @byte, int bit)
            => (byte)(@byte | (1 << bit));

        private static byte WithoutBit(byte @byte, int bit)
            => (byte)(@byte & ~(1 << bit));

    }    
}