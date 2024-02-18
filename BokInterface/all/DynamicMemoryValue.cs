namespace BokInterface.All
{

    /// <summary>Class for representing a dynamic memory address' value</summary>
    public class DynamicMemoryValue
    {

        public string name;
        private readonly uint firstAddress;
        private readonly uint secondAddress;

        public uint Value
        {
            get => Utilities.ReadDynamicAddress(firstAddress, secondAddress);
            set => Utilities.WriteDynamicAddress(value, firstAddress, secondAddress);
        }

        public DynamicMemoryValue(string name, uint firstAddress, uint secondAddress)
        {
            this.name = name;
            this.firstAddress = firstAddress;
            this.secondAddress = secondAddress;
        }
    }
}