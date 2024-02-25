namespace BokInterface.All {

    /// <summary>Class for representing a U16 memory address' value</summary>
    public class U16MemoryValue {

        public string name;
        private readonly uint address;

        public uint Value {
            get => APIs.Memory.ReadU16(address);
            set => APIs.Memory.WriteU16(address, value);
        }

        public U16MemoryValue(string name, uint address) {
            this.name = name;
            this.address = address;
        }
    }
}