using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai 2: Solar Boy Django / Zoktai memory addresses</summary>
    public class ZoktaiAddresses {

        /// <summary>
        /// <para>Django-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Django = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Inventory-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Magics-related memory addresses</para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Magics = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        public ZoktaiAddresses() {

            // For less repetition
            string note = "";

            // Add Django addresses
            Django.Add("x_position", new MemoryAddress(0x30, note: "Django X position"));
            Django.Add("y_position", new MemoryAddress(0x34, note: "Django Y position"));
            Django.Add("z_position", new MemoryAddress(0x32, note: "Django Z position"));

            // Current stats
            note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Django.Add("current_hp", new MemoryAddress(0x364, note: note));
            Django.Add("current_ene", new MemoryAddress(0x368, note: note));
            Django.Add("current_vit", new MemoryAddress(0x35C, note: note));
            Django.Add("current_spr", new MemoryAddress(0x35E, note: note));
            Django.Add("current_str", new MemoryAddress(0x360, note: note));
            Django.Add("current_agi", new MemoryAddress(0x362, note: note));

            /**
             * Persistent stats (used on screen transitions & save data)
             * Note : For some stats, "current" can be 1 higher than "persistent", unsure why
             */
            note = "Also corresponds to values from Save Data";
            Django.Add("persistent_hp", new MemoryAddress(0x28, note: note));
            Django.Add("persistent_ene", new MemoryAddress(0x2C, note: note));
            Django.Add("persistent_vit", new MemoryAddress(0x18, note: note));
            Django.Add("persistent_spr", new MemoryAddress(0x1A, note: note));
            Django.Add("persistent_str", new MemoryAddress(0x1C, note: note));
            Django.Add("persistent_agi", new MemoryAddress(0x1E, note: note));

            // EXP & level
            Django.Add("level", new MemoryAddress(0x40));
            Django.Add("exp", new MemoryAddress(0x50, type: "U32"));
            Django.Add("total_exp_until_next_level", new MemoryAddress(0x02002464, type: "U32"));

            // Skill
            note = "100 skill exp = 1 lvl";
            Django.Add("sword_skill_exp", new MemoryAddress(0x46, note: note));
            Django.Add("spear_skill_exp", new MemoryAddress(0x48, note: note));
            Django.Add("hammer_skill_exp", new MemoryAddress(0x4A, note: note));
            Django.Add("fists_skill_exp", new MemoryAddress(0x4C, note: note));
            Django.Add("gun_skill_exp", new MemoryAddress(0x4E, note: note));

            // Stat points
            Django.Add("showned_stat_points_to_allocate", new MemoryAddress(0x02006E20)); // useless
            Django.Add("stat_points_to_allocate", new MemoryAddress(0x42));

            // Add Misc addresses
            Misc.Add("stat", new MemoryAddress(0x030046A0, note: "For persistent stats & inventory", type: "U32", domain: "IWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x03004698, note: "Story progress & dungeon states", type: "U32", domain: "IWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x03004690, type: "U32", domain: "IWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030046A4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x030047C8, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x030047CA, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x030047CC, note: "Camera Z position", domain: "IWRAM"));
            Misc.Add("current_stat", new MemoryAddress(0x03002BE0, note: "For current stats", type: "U32", domain: "IWRAM"));

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

            // Misc.Add("exp_table", new MemoryAddress(0x08ce3238));
        }
    }
}