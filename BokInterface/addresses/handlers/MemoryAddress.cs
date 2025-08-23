using BokInterface.Utils;

namespace BokInterface.Addresses {
    /// <summary>Class for representing a memory address and its informations</summary>
    /// <param name="address">Memory address</param>
    /// <param name="note">Note regarding the address</param>
    /// <param name="type">Type (by default U16)</param>
    /// <param name="domain">Domain (by default none is specified because it is not always necessary)</param>
    public class MemoryAddress(uint address, string note = "", string type = "U16", string? domain = null) {
        public uint Address = address;
        public readonly string Type = type;
        /// <summary>
        ///     If the address is 8 characters or more, it already specifies the domain.<br/>
        ///     <i>In that case the domain will not be passed to the value reading and writing methods to prevent issues.</i>
        /// </summary>
        public readonly string? Domain = domain;
        public readonly string Note = note;
        private readonly int _length = address.ToString().Length;
        public uint Value {
            get => Utilities.ReadMemoryAddress(Address, Type, _length >= 8 ? null : Domain);
            set => Utilities.WriteMemoryAddress(Address, value, Type, _length >= 8 ? null : Domain);
        }
    }
}
