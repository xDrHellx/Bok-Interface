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

            /**
             * Used for setting "current" values (which are used for damage calculations)
             * These must be combined with the "stat" memory address' value
             */
            Django.Add("current_hp", 0x364);
            Django.Add("current_ene", 0x368);
            Django.Add("current_vit", 0x35C);
            Django.Add("current_spr", 0x35E);
            Django.Add("current_str", 0x360);
            Django.Add("current_agi", 0x362);

            /**
             * "Persistent" values, these also correspond to the values from save data
             * "Current" values will be copied to these on screen transition
             * 
             * Note : For some stats, "current" can be 1 higher than "persistent", unsure why
             */
            Django.Add("persistent_hp", 0x0203C428);
            Django.Add("persistent_ene", 0x0203C42C);
            Django.Add("persistent_vit", 0x0203C418);
            Django.Add("persistent_spr", 0x0203C41A);
            Django.Add("persistent_str", 0x0203C41C);
            Django.Add("persistent_agi", 0x0203C41E);

            Django.Add("sword_skill", 0x0203C446);
            Django.Add("spear_skill", 0x0203C448);
            Django.Add("hammer_skill", 0x0203C44A);
            Django.Add("fists_skill", 0x0203C44C);
            Django.Add("gun_skill", 0x0203C44E);

            // Add Misc addresses
            Misc.Add("map_data", 0x030046A4);
            Misc.Add("x_camera", 0x030047C8);
            Misc.Add("y_camera", 0x030047CA);
            Misc.Add("z_camera", 0x030047CC);
            Misc.Add("stat", 0x03002BE0);

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