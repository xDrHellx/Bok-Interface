namespace BokInterface.Addresses {
    /// <summary>Class for representing a memory address and its informations</summary>
    /// <param name="address">Memory address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default EWRAM)</param>
    /// <param name="note">Note regarding the address</param>
    /// <returns><c>Dictionary<string, object></c>Object with informations</returns>
    public class MemoryAddress(uint address, string type = "U16", string domain = "EWRAM", string note = "") {
        public readonly uint address = address;
        public readonly string type = type;
        public readonly string domain = domain;
        public readonly string note = note;
    }
}