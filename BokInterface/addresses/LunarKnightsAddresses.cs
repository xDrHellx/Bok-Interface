using System.Collections.Generic;

namespace BokInterface.Addresses {
    /// <summary>Main class for Boktai DS / Lunar Knights memory addresses</summary>
    public class LunarKnightsAddresses {

        /// <summary>Aaron-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Aaron = new Dictionary<string, MemoryAddress>();

        /// <summary>Lucian-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Lucian = new Dictionary<string, MemoryAddress>();

        /// <summary>Inventory-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>
        ///     <para>Misc memory addresses</para>
        ///     <para>
        ///         These are used in combination with other memory addresses to get / set values that are "dynamic."<br/>
        ///         <i>For example the memory address for Django's current HP is different based on which "room sections" he is in.</i>
        ///     </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        public LunarKnightsAddresses() { }
    }
}
