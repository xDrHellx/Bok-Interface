namespace BokInterface.All {

    /// <summary>Class for representing a U16 memory address' value</summary>
    public class U16MemoryValue(string name, uint address) {

        public string name = name;
        private readonly uint _address = address;

        public uint Value {
            get => APIs.Memory.ReadU16(_address);
            set => APIs.Memory.WriteU16(_address, value);
        }
    }
}