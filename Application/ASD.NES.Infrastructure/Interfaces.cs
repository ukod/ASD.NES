using System;
using System.Collections.Generic;
using System.Linq;

// propotyping


namespace ASD.NES.Infrastructure {

    // logic

    public interface IProcessingUnit {
        void Step();
        void Step(int times);
    }

    // memory management

    public abstract class Memory<T> : IMemory<T> where T : struct {

        private IList<T> memory;
        private IReadOnlyCollection<RedirectRule> rules;

        public T this[int address] {
            get => memory[Redirect(address)];
            set => memory[Redirect(address)] = value;
        }

        private int Redirect(int address)
            => rules.First(r => r.SourceAddress == address).TargetAddress;

        // configure
        // add offset
        // remap single address
        // remap range
        // ... move this to separate mapper? and after configure mem.mapper = mapper?


        private void Test() {

        }

    }

    public interface IMemory<T> where T : struct {
        T this[int address] { get; set; }
    }

    public struct RedirectRule {
        public int SourceAddress { get; }
        public int TargetAddress { get; }
        public RedirectRule(int sourceAddress, int targetAddress) {
            SourceAddress = sourceAddress; TargetAddress = targetAddress;
        }
    }
}