using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai: The Sun is in Your Hand memory addresses</summary>
    public class BoktaiAddresses {

        /// <summary>
        /// <para>Django-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Django = new Dictionary<string, uint>();
        
        /// <summary>
        /// <para>Inventory-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Inventory = new Dictionary<string, uint>();

        /// <summary>
        /// <para>Garding-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Gardening = new Dictionary<string, uint>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, uint> Misc = new Dictionary<string, uint>();

        public BoktaiAddresses() {

            // Add Django addresses
            Django.Add("x_position", 0x0203D8F0);
            Django.Add("y_position", 0x0203D8F4);
            Django.Add("z_position", 0x0203D8F2);
            
            // Add Misc addresses
            Misc.Add("map_data", 0x03004610);
            Misc.Add("x_camera", 0x030046E8);
            Misc.Add("y_camera", 0x030046EA);
            Misc.Add("z_camera", 0x030046EC);
        }
    }
}