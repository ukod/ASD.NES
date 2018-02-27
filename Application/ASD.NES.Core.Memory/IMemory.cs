namespace ASD.NES.Core.Memory {

    public interface IMemory<T> {
        T this[int address] { get; set; }
    }
}