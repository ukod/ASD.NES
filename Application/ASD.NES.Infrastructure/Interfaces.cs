using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// propotyping

namespace ASD.NES.Infrastructure {

    // logic

    public interface IProcessingUnit {
        void Step();
        void Step(int times);
    }

    public abstract class ProcessingUnit : IProcessingUnit {

        protected IMemory<byte> memory;

        public abstract void Step();
        public abstract void Step(int times);
    }
}

namespace ASD.NES.Infrastructure {

    // memory map

    


    // memory management

    public abstract class Memory<T> : IMemory<T> where T : struct {

        private IList<T> mem;
        private IMapper map;

        public T this[int address] {
            get => mem[map.Redirect(address)];
            set => mem[map.Redirect(address)] = value;
        }

        public Memory(IMapper mapper = null) {
            map = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} can't be null");
            mem = new T[(map.MaxAddress + 1) - map.MinAddress];
        }
    }

    public interface IMemory<T> where T : struct {
        T this[int address] { get; set; }
    }

    public interface IMapper {
        int MinAddress { get; }
        int MaxAddress { get; }
        int Redirect(int address);
    }

    public class Mapper : IMapper {

        private int min, max; // calc auto

        public int MinAddress => min;
        public int MaxAddress => max;

        public int Redirect(int address) {

            var ra = default(int);
            rules.FirstOrDefault(r => (ra = r(address)) > -1);

            return ra >= 0 ? ra : throw new IndexOutOfRangeException($"{nameof(address)} is out of range");
        }

        //public void AddRule(int source, int target) {
        //    rules.Add(new Rule { Source = source, Target = target });
        //}

        private List<Func<int, int>> rules = new List<Func<int, int>>();

        public void AddRangeRule(int min, int max, int offset) {
            rules.Add((address)
                => address >= min && address <= max
                ? offset + (address - min)
                : -1);
        }

        public void AddRangeRule(int start, int count) {

        }
    }

    public class MapperRange {
        int MinAddress { get; }
        int MaxAddress { get; }

        public int Redirect(int address) {
            if (address >= MinAddress && address <= MaxAddress) {
                return address - MinAddress;
            }
            return -1;
        }

    }

    public struct RedirectRule {
        public int SourceAddress { get; }
        public int TargetAddress { get; }
        public RedirectRule(int sourceAddress, int targetAddress) {
            SourceAddress = sourceAddress; TargetAddress = targetAddress;
        }
    }
}