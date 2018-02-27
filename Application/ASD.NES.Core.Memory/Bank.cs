using System;
using System.Collections.Generic;
using System.Text;

namespace ASD.NES.Core.Memory {

    public sealed class Bank : IMemory<byte> {

        private Cell[] data;

        public byte this[int address] {
            get => data[address].Value;
            set => data[address].Value = value;
        }

        public Bank() {

        }

        public static IMemory<byte> GetMemory() {

        }
    }
}