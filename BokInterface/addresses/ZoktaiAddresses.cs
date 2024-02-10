using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai 2: Solar Boy Django / Zoktai memory addresses</summary>
    public class ZoktaiAddresses {

        /// <summary>
        /// <para>Django-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Django = new Dictionary<string, uint>();
        
        /// <summary>
        /// <para>Inventory-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Inventory = new Dictionary<string, uint>();

        /// <summary>
        /// <para>Magics-related memory addresses</para>
        /// </summary>
        public IDictionary<string, uint> Magics = new Dictionary<string, uint>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, uint> Misc = new Dictionary<string, uint>();

        public ZoktaiAddresses() {
            
            // Add Django addresses
            Django.Add("x_position", 0x0203C430);
            Django.Add("y_position", 0x0203C434);
            Django.Add("z_position", 0x0203C432);

            // Add Misc addresses
            Misc.Add("map_data", 0x030046a4);

            // 0x203C650 django's current form
            /*
                0 - Django
                1 - Black Django
                2 - Bat
                3 - Mouse
                4 - Unused
                5 - Sabata
            */
        }
    }
}