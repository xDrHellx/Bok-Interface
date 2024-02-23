namespace BokInterface.All {

    /// <summary>Class for representing a U16 memory address' value</summary>
    public class MemoryValueU16 {

        public string Name;
        private readonly uint address;

        public uint Value {
            get => APIs.Memory.ReadU16(address);
            set => APIs.Memory.WriteU16(value, address);
        }

        public MemoryValueU16(string Name, uint address) {
            this.Name = Name;
            this.address = address;
        }
    }
}