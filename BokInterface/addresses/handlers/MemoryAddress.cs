using BokInterface.All;

namespace BokInterface.Addresses {
    /// <summary>Class for representing a memory address and its informations</summary>
    /// <param name="address">Memory address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default EWRAM)</param>
    /// <param name="note">Note regarding the address</param>
    public class MemoryAddress(uint address, string type = "U16", string domain = "EWRAM", string note = "") {
        public readonly uint address = address;
        public readonly string type = type;
        public readonly string domain = domain;
        public readonly string note = note;
        public uint Value {
            get => Utilities.ReadMemoryAddress(address, type, domain);
            set => Utilities.WriteMemoryAddress(address, value, type, domain);
        }
    }
}