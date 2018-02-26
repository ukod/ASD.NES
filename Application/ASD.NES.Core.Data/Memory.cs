using System;
using System.Collections.Generic;

// for redirect

// case: bin search: 16 + 14 iter   per r\w on 64kb + 16kb    O(log(n))  // clean arch, use redirect list with all addresses
// case: rules as func: ~50 invokes per r\w on 64kb + 16kb    Const      // clean arch, use redirect list with range functions (arithmetic or conditions)
// case: wrap every byte in class,  per r\w on 64kb + 16kb    O(1)       // dirty code, use links and proxies

namespace ASD.NES.Core.Data {

    public sealed class CPUAddressSpace {

        private static readonly byte[] memory = new byte[0x10000]; // 64kb

        // proxies

        //public IMemory<byte> ZepoPage { get; }
        //public IMemory<byte> Stack { get; }
        //public IMemory<byte> WRAM { get; }
        //public IMemory<byte> RegistersPPU { get; }
        //public IMemory<byte> RegistersAPU { get; }

    }
    public sealed class PPUAddressSpace {
        private static readonly byte[] memory = new byte[0x4000];  // 16kb
    }


    internal abstract class MemoryBank {

    }

    public abstract class Memory<T> : IMemory<T> where T : struct {

        private IList<T> mem;
        private IMapper map;

        public T this[int address] {
            get => mem[map.Redirect(address)];
            set => mem[map.Redirect(address)] = value;
        }

        internal Memory(IMapper mapper = null) {
            map = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} can't be null");
            mem = new T[map.WorkingAreaSize];
        }
    }

    public abstract class Mapper : IMapper {
        public int WorkingAreaSize => 0;
        public int Redirect(int address) => 0;
    }










    public interface IMemory<T> where T : struct {
        T this[int address] { get; set; }
    }

    public interface IMapper {
        int WorkingAreaSize { get; }
        int Redirect(int address);
    }
}