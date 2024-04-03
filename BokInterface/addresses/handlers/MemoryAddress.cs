using BokInterface.All;

namespace BokInterface.Addresses {
    /// <summary>Class for representing a memory address and its informations</summary>
    /// <param name="address">Memory address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default EWRAM)</param>
    /// <param name="note">Note regarding the address</param>
    public class MemoryAddress(uint address, string type = "U16", string domain = "EWRAM", string note = "") {
        public readonly uint Address = address;
        public readonly string Type = type;
        public readonly string Domain = domain;
        public readonly string Note = note;
        public uint Value {
            get => Utilities.ReadMemoryAddress(Address, Type, Domain);
            set => Utilities.WriteMemoryAddress(Address, value, Type, Domain);
        }
    }
}