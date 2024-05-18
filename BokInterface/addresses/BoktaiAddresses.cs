using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai: The Sun is in Your Hand memory addresses</summary>
    public class BoktaiAddresses {

        /// <summary>
        /// <para>Django-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Django = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Inventory-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Garding-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Gardening = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        public BoktaiAddresses() {

            // Add Django addresses
            Django.Add("x_position", new MemoryAddress(0x0203D8F0, note: "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x0203D8F4, note: "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x0203D8F2, note: "Django Z position", domain: "EWRAM"));

            // Add Misc addresses
            Misc.Add("map_data", new MemoryAddress(0x03004610, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x030046E8, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x030046EA, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x030046EC, note: "Camera Z position", domain: "IWRAM"));
        }
    }
}