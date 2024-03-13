namespace BokInterface.All {

    /// <summary>Class for representing a dynamic memory address' value</summary>
    public class DynamicMemoryValue(string name, uint firstAddress, uint secondAddress) {

        public string name = name;
        private readonly uint _firstAddress = firstAddress;
        private readonly uint _secondAddress = secondAddress;

        public uint Value {
            get => Utilities.ReadDynamicAddress(_firstAddress, _secondAddress);
            set => Utilities.WriteDynamicAddress(value, _firstAddress, _secondAddress);
        }
    }
}