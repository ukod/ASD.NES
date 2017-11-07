﻿namespace ASD.NES.Core.ConsoleComponents.CPUParts {

    using Shared;

    /// <summary> "Indirect Y" (Post-Indexed Indirect) addressing mode </summary>
    internal sealed class IDY : AddressingModeOld {
        public override Hextet Address => (ushort)(Hextet.Make(memory[ArgOne + 1], memory[ArgOne]) + r.Y);
        public override Octet M { get => memory[Address]; set => memory[Address] = value; }
        public override bool PageCrossed => !SamePage(Address, (ushort)(Address - r.Y));
        public IDY(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Indirect X" (Pre-Indexed Indirect) addressing mode </summary>
    internal sealed class IDX : AddressingModeOld {
        public override Hextet Address => Hextet.Make(memory[ArgOne + 1 + r.X], memory[ArgOne + r.X]);
        public override Octet M { get => memory[Address]; set => memory[Address] = value; }
        public IDX(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Indirect" addressing mode </summary>
    internal sealed class IND : AddressingModeOld {
        public override Hextet Address => Hextet.Make(memory[ArgOne + 1], memory[ArgOne]);
        public override Octet M { get => memory[Address]; set => memory[Address] = value; }
        public IND(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Absolute Y" addressing mode </summary>
    internal sealed class ABY : ABS {
        public override Hextet Address => (ushort)(base.Address + r.Y);
        public override bool PageCrossed => !SamePage(Address, base.Address);
        public ABY(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Absolute X" addressing mode </summary>
    internal sealed class ABX : ABS {
        public override Hextet Address => (ushort)(base.Address + r.X);
        public override bool PageCrossed => !SamePage(Address, base.Address);
        public ABX(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Absolute" addressing mode </summary>
    internal class ABS : AddressingModeOld {
        public override Hextet Address => Hextet.Make(ArgTwo, ArgOne);
        public override Octet M { get => memory[Address]; set => memory[Address] = value; }
        public ABS(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Zero page (Post-Indexed) Y" addressing mode </summary>
    internal sealed class ZPY : ZPG {
        public override Hextet Address => (ushort)(base.Address + r.Y);
        public ZPY(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Zero page (Post-Indexed) X" addressing mode </summary>
    internal sealed class ZPX : ZPG {
        public override Hextet Address => (ushort)(base.Address + r.X);
        public ZPX(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Zero page" addressing mode </summary>
    internal class ZPG : AddressingModeOld {
        public override Hextet Address => ArgOne;
        public override Octet M { get => memory[Address]; set => memory[Address] = value; }
        public ZPG(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Relative" addressing mode </summary>
    internal sealed class REL : AddressingModeOld {
        public override Hextet Address { get => r.PC; set => r.PC = value; }
        public REL(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Immediate" addressing mode </summary>
    internal sealed class IMM : AddressingModeOld {
        public override Octet M => ArgOne;
        public IMM(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Accumulator" addressing mode </summary>
    internal sealed class ACC : AddressingModeOld {
        public override Octet M { get => r.A; set => r.A = value; }
        public ACC(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> "Implied" addressing mode </summary>
    internal sealed class IMP : AddressingModeOld {
        public IMP(RegistersCPU registers) : base(registers) { }
    }

    /// <summary> Addressing mode </summary>
    internal abstract class AddressingModeOld {

        protected readonly CPUAddressSpace memory = CPUAddressSpace.Instance;
        protected readonly RegistersCPU r;

        protected virtual byte ArgOne => memory[r.PC + 1];
        protected virtual byte ArgTwo => memory[r.PC + 2];

        public virtual Hextet Address { get; set; }
        public virtual bool PageCrossed => false;
        public virtual Octet M { get; set; }

        public AddressingModeOld(RegistersCPU registers)
            => r = registers;

        public bool SamePage(ushort addressA, ushort addressB)
            => (addressA & 0xFF00) == (addressB & 0xFF00);
    }
}