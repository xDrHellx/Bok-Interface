namespace BokInterface.All {

    /// <summary>Class for representing a memory address' value</summary>
    /// <param name="address">Memory address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default EWRAM)</param>
    /// <param name="note">Note regarding the address</param>
    public class MemoryValue(string name, uint address, string type = "u16", string domain = "EWRAM") {

        public string name = name;
        private readonly uint address = address;
        private readonly string type = type;
        private readonly string domain = domain;

        public uint Value {
            get => Utilities.ReadMemoryAddress(address, type, domain);
            set => Utilities.WriteMemoryAddress(address, value, type, domain);
        }
    }
}