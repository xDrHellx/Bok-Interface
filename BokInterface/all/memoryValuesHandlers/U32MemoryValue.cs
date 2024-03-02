namespace BokInterface.All {

    /// <summary>Class for representing a U32 memory address' value</summary>
    public class U32MemoryValue(string name, uint address) {

        public string name = name;
        private readonly uint address = address;

        public uint Value {
            get => APIs.Memory.ReadU32(address);
            set => APIs.Memory.WriteU32(address, value);
        }
    }
}