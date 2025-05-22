using System.Collections.Generic;

namespace BokInterface.Addresses {
    /// <summary>Main class for Boktai DS / Lunar Knights memory addresses</summary>
    public class LunarKnightsAddresses {

        /// <summary>
        /// <para>Aaron-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Aaron = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Lucian-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Lucian = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Inventory-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        public LunarKnightsAddresses() {

        }
    }
}