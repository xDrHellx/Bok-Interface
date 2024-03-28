namespace BokInterface.All {

    /// <summary>Class for representing a dynamic memory address' value</summary>
    /// <param name="name">Name for the instance</param>
    /// <param name="firstAddress">First address</param>
    /// <param name="secondAddress">Second address</param>
    /// <param name="type">Type of method to use for reading or writing to the result of both addresses (by default "U16" because it is the most common one)</param>
    public class DynamicMemoryValue(string name, uint firstAddress, uint secondAddress, string type = "U16") {

        public string name = name;
        private readonly uint _firstAddress = firstAddress;
        private readonly uint _secondAddress = secondAddress;
        public string _type = type;

        public uint Value {
            get => Utilities.ReadDynamicAddress(_firstAddress, _secondAddress, _type);
            set => Utilities.WriteDynamicAddress(value, _firstAddress, _secondAddress, _type);
        }
    }
}