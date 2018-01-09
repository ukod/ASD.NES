﻿namespace ASD.NES.Kernel.ConsoleComponents.APUParts.Registers {

    // TODO: Separate channel & registers

    using BasicComponents;
    using Helpers;

    internal sealed class TriangleChannel : IMemory<byte> {

        private byte[] register = new byte[4];
        public byte this[int address] {
            get => register[address & 3];
            set => register[address & 3] = value;
        }
        public int Cells => register.Length;

        // ------- Registers -------

        // register[0] - $4008 : CRRR RRRR : Length counter halt / linear counter control(C), linear counter load(R)
        public bool LengthCounterHalt => register[0].HasBit(7);
        public byte LinearCounterLoad {
            get => (byte)(register[0] & 0x7F);
            set => register[0] = (byte)((register[0] & 0x80) | (value & 0x7F));
        }

        // register[1] - $4009 : ---- ---- : Unused
        // register[2] - $400A : TTTT TTTT : Timer low(T)
        // register[3] - $400B : LLLL LTTT : Length counter load(L), timer high(T)

        public ushort Timer {
            get => (ushort)(((register[3] & 0b111) << 8) | register[2]);
            set {
                register[2] = (byte)value;
                register[3] = (byte)((register[3] | 0b1111_1000) | ((value >> 8) & 0b111));
            }
        }

        public byte LengthCounterLoad => (byte)(register[3] >> 3);


        // ------- Additional -------

        public int CurrentLengthCounter { get; set; }
        public int CurrentLinearCounter { get; set; }

        public void TickLengthCounter() {
            if (!LengthCounterHalt) {
                CurrentLengthCounter -= 1;
                if (CurrentLengthCounter < 0) {
                    CurrentLengthCounter = 0;
                }
            }
        }

        public void TickLinearCounter() {
            if (!LengthCounterHalt) {
                if (LinearCounterLoad == 0) {
                    LinearCounterLoad = 0;
                }
                else {
                    LinearCounterLoad -= 1;
                }
            }
        }

        public float GetTriangleAudio(int timeInSamples, int sampleRate) {

            // One octave lower than pulse frequency
            var frequency = 111860.0 / Timer / 2;
            var normalizedSampleTime = timeInSamples * frequency / sampleRate;

            var normalized = ((timeInSamples * (int)frequency) % sampleRate) / (float)sampleRate;

            // Map [0,1) to the triangle in range [-1,1]
            if (normalized <= 0.5) {
                return -1f + 4f * normalized;
            }
            else {
                return 3f - 4f * normalized;
            }
        }
    }
}