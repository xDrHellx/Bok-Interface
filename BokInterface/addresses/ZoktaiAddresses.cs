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
            Django.Add("x_position", 0x30); // A
            Django.Add("y_position", 0x34); // A
            Django.Add("z_position", 0x32); // A

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
            Django.Add("persistent_hp", 0x28); // A
            Django.Add("persistent_ene", 0x2C); // A
            Django.Add("persistent_vit", 0x18); // A
            Django.Add("persistent_spr", 0x1A); // A
            Django.Add("persistent_str", 0x1C); // A
            Django.Add("persistent_agi", 0x1E); // A

            // EXP & level
            Django.Add("level", 0x40); // A
            Django.Add("exp", 0x0203C450); // A
            Django.Add("total_exp_until_next_level", 0x02002464);

            // 100 skill exp = 1 lvl
            Django.Add("sword_skill_exp", 0x46); // A
            Django.Add("spear_skill_exp", 0x48); // A
            Django.Add("hammer_skill_exp", 0x4A); // A
            Django.Add("fists_skill_exp", 0x4C); // A
            Django.Add("gun_skill_exp", 0x4E); // A

            // Stat points
            Django.Add("showned_stat_points_to_allocate", 0x02006E20); // useless
            Django.Add("stat_points_to_allocate", 0x42); // A

            // Add Misc addresses
            Misc.Add("stat", 0x030046A0); // inventory too
            Misc.Add("world_state", 0x03004698); // Story progress, dungeon states, ...
            Misc.Add("scratch", 0x03004690);
            Misc.Add("map_data", 0x030046A4);
            Misc.Add("x_camera", 0x030047C8);
            Misc.Add("y_camera", 0x030047CA);
            Misc.Add("z_camera", 0x030047CC);
            Misc.Add("current_stat", 0x03002BE0);

            // 0x203C650 django's current form
            /*
                0 - Django
                1 - Black Django
                2 - Bat
                3 - Mouse
                4 - Unused
                5 - Sabata
            */

            /*
             * US version 
             */

            // Misc.Add("exp_table", 0x08ce3238);
        }
    }
}