namespace BokInterface.All {

    /// <summary>Class for representing a dynamic memory address' value</summary>
    /// <param name="name">Name for the instance</param>
    /// <param name="firstAddress">First address</param>
    /// <param name="secondAddress">Second address</param>
    /// <param name="type">Type of method to use for reading or writing to the result of both addresses (by default "U16" because it is the most common one)</param>
    public class DynamicMemoryValue(string name, uint firstAddress, uint secondAddress, string type = "U16") {

        public string name = name;
        private readonly uint firstAddress = firstAddress;
        private readonly uint secondAddress = secondAddress;
        public string type = type;

        public uint Value {
            get => Utilities.ReadDynamicAddress(firstAddress, secondAddress, type);
            set => Utilities.WriteDynamicAddress(value, firstAddress, secondAddress, type);
        }
    }
}