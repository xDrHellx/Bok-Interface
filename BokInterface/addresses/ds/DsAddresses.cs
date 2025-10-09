using System.Collections.Generic;

namespace BokInterface.Addresses {
    /// <summary>Abstract class for Boktai DS / Lunar Knights memory addresses</summary>
    public abstract class DsAddresses {
        /// <summary>Inventory-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();
    }
}
