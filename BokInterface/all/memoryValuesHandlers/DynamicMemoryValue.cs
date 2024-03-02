namespace BokInterface.All {

    /// <summary>Class for representing a dynamic memory address' value</summary>
    public class DynamicMemoryValue(string name, uint firstAddress, uint secondAddress) {

        public string name = name;
        private readonly uint firstAddress = firstAddress;
        private readonly uint secondAddress = secondAddress;

        public uint Value {
            get => Utilities.ReadDynamicAddress(firstAddress, secondAddress);
            set => Utilities.WriteDynamicAddress(value, firstAddress, secondAddress);
        }
    }
}