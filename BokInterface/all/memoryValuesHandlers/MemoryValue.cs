namespace BokInterface.All {

    /// <summary>Class for representing a memory address' value</summary>
    /// <param name="address">Memory address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default none is specified because it is not always necessary)</param>
    /// <param name="note">Note regarding the address</param>
    public class MemoryValue(string name, uint address, string type = "u16", string? domain = null) {

        public string Name = name;
        private readonly uint Address = address;
        private readonly string Type = type;
        private readonly string? Domain = domain;

        public uint Value {
            get => Utilities.ReadMemoryAddress(Address, Type, Domain);
            set => Utilities.WriteMemoryAddress(Address, value, Type, Domain);
        }
    }
}