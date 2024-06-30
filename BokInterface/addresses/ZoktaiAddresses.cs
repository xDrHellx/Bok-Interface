using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai 2: Solar Boy Django / Zoktai memory addresses</summary>
    public class ZoktaiAddresses {

        /// <summary>Django-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Django = new Dictionary<string, MemoryAddress>();

        /// <summary>Sabata-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Sabata = new Dictionary<string, MemoryAddress>();

        /// <summary>Inventory-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>Magics-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Magics = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic"<br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        /// <summary>Note for MemoryAddress instances (for less repetition)</summary>
        private string _note = "";

        public ZoktaiAddresses() {
            InitPlayableCharactersAddresses();
            InitInventoryAddresses();
            InitMiscAddresses();
        }

        protected void InitPlayableCharactersAddresses() {

            // Add Django addresses
            Django.Add("x_position", new MemoryAddress(0x30, note: "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x34, note: "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x32, note: "Django Z position", domain: "EWRAM"));

            // Current stats
            _note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Django.Add("current_hp", new MemoryAddress(0x364, note: _note, domain: "EWRAM"));
            Django.Add("current_ene", new MemoryAddress(0x368, note: _note, domain: "EWRAM"));
            Django.Add("current_vit", new MemoryAddress(0x35C, note: _note, domain: "EWRAM"));
            Django.Add("current_spr", new MemoryAddress(0x35E, note: _note, domain: "EWRAM"));
            Django.Add("current_str", new MemoryAddress(0x360, note: _note, domain: "EWRAM"));
            Django.Add("current_agi", new MemoryAddress(0x362, note: _note, domain: "EWRAM"));

            /**
             * Persistent stats (used on screen transitions & save data)
             * Note : For some stats, "current" can be 1 higher than "persistent", unsure why
             */
            _note = "Also corresponds to values from Save Data";
            Django.Add("persistent_hp", new MemoryAddress(0x28, note: _note, domain: "EWRAM"));
            Django.Add("persistent_ene", new MemoryAddress(0x2C, note: _note, domain: "EWRAM"));
            Django.Add("persistent_vit", new MemoryAddress(0x18, note: _note, domain: "EWRAM"));
            Django.Add("persistent_spr", new MemoryAddress(0x1A, note: _note, domain: "EWRAM"));
            Django.Add("persistent_str", new MemoryAddress(0x1C, note: _note, domain: "EWRAM"));
            Django.Add("persistent_agi", new MemoryAddress(0x1E, note: _note, domain: "EWRAM"));

            // EXP & level
            Django.Add("level", new MemoryAddress(0x40, domain: "EWRAM"));
            Django.Add("exp", new MemoryAddress(0x50, type: "U32", domain: "EWRAM"));
            Django.Add("total_exp_until_next_level", new MemoryAddress(0x2464, type: "U32", domain: "EWRAM"));

            // Skill
            _note = "100 skill exp = 1 lvl";
            Django.Add("sword_skill_exp", new MemoryAddress(0x46, note: _note, domain: "EWRAM"));
            Django.Add("spear_skill_exp", new MemoryAddress(0x48, note: _note, domain: "EWRAM"));
            Django.Add("hammer_skill_exp", new MemoryAddress(0x4A, note: _note, domain: "EWRAM"));
            Django.Add("fists_skill_exp", new MemoryAddress(0x4C, note: _note, domain: "EWRAM"));
            Django.Add("gun_skill_exp", new MemoryAddress(0x4E, note: _note, domain: "EWRAM"));

            // Stat points
            // Django.Add("showned_stat_points_to_allocate", new MemoryAddress(0x02006E20)); // useless
            Django.Add("stat_points_to_allocate", new MemoryAddress(0x42, domain: "EWRAM"));

            // Status effects
            _note = "Kaamos status is applied when this value is > 35999 & it rains";
            Django.Add("kaamos_status", new MemoryAddress(0x2C8, domain: "EWRAM"));
            Django.Add("kaamos_progress", new MemoryAddress(0x2C0, note: _note, domain: "EWRAM"));

            // Sabata
            Sabata.Add("kaamos_status", new MemoryAddress(0x02CE, domain: "EWRAM"));
            Sabata.Add("kaamos_progress", new MemoryAddress(0x2C4, note: _note, domain: "EWRAM"));

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

        protected void InitInventoryAddresses() {

            // Items inventory
            _note = "Item slot";
            Inventory.Add("item_slot_1", new MemoryAddress(0x70, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_2", new MemoryAddress(0x72, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_3", new MemoryAddress(0x74, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_4", new MemoryAddress(0x76, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_5", new MemoryAddress(0x78, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_6", new MemoryAddress(0x7A, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_7", new MemoryAddress(0x7C, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_8", new MemoryAddress(0x7E, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_9", new MemoryAddress(0x80, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_10", new MemoryAddress(0x82, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_11", new MemoryAddress(0x84, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_12", new MemoryAddress(0x86, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_13", new MemoryAddress(0x88, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_14", new MemoryAddress(0x8A, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_15", new MemoryAddress(0x8C, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_16", new MemoryAddress(0x8E, note: _note, domain: "EWRAM"));

            // Items durability
            _note = "Item durability";
            Inventory.Add("item_slot_durability_1", new MemoryAddress(0xD0, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_2", new MemoryAddress(0xD2, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_3", new MemoryAddress(0xD4, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_4", new MemoryAddress(0xD6, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_5", new MemoryAddress(0xD8, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_6", new MemoryAddress(0xDA, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_7", new MemoryAddress(0xDC, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_8", new MemoryAddress(0xDE, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_9", new MemoryAddress(0xE0, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_10", new MemoryAddress(0xE2, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_11", new MemoryAddress(0xE4, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_12", new MemoryAddress(0xE6, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_13", new MemoryAddress(0xE8, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_14", new MemoryAddress(0xEA, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_15", new MemoryAddress(0xEC, note: _note, domain: "EWRAM"));
            Inventory.Add("item_slot_durability_16", new MemoryAddress(0xEE, note: _note, domain: "EWRAM"));

            // Key items inventory
            _note = "Key item inventory slot";
            Inventory.Add("key_item_slot_1", new MemoryAddress(0x130, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_2", new MemoryAddress(0x132, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_3", new MemoryAddress(0x134, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_4", new MemoryAddress(0x136, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_5", new MemoryAddress(0x138, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_6", new MemoryAddress(0x13A, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_7", new MemoryAddress(0x13C, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_8", new MemoryAddress(0x13E, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_9", new MemoryAddress(0x140, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_10", new MemoryAddress(0x142, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_11", new MemoryAddress(0x144, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_12", new MemoryAddress(0x146, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_13", new MemoryAddress(0x148, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_14", new MemoryAddress(0x14A, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_15", new MemoryAddress(0x14C, note: _note, domain: "EWRAM"));
            Inventory.Add("key_item_slot_16", new MemoryAddress(0x14E, note: _note, domain: "EWRAM"));

            // Weapons inventory
            _note = "Weapon inventory slot";
            Inventory.Add("weapon_slot_1", new MemoryAddress(0x3D0, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2", new MemoryAddress(0x3EC, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3", new MemoryAddress(0x408, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4", new MemoryAddress(0x424, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5", new MemoryAddress(0x440, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6", new MemoryAddress(0x45C, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7", new MemoryAddress(0x478, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8", new MemoryAddress(0x494, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9", new MemoryAddress(0x4B0, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10", new MemoryAddress(0x4CC, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11", new MemoryAddress(0x4E8, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12", new MemoryAddress(0x504, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13", new MemoryAddress(0x520, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14", new MemoryAddress(0x53C, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15", new MemoryAddress(0x558, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16", new MemoryAddress(0x574, note: _note, domain: "EWRAM"));
        }

        protected void InitMiscAddresses() {

            Misc.Add("stat", new MemoryAddress(0x030046A0, note: "For persistent stats & inventory", type: "U32", domain: "IWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x03004698, note: "Story progress & dungeon states", type: "U32", domain: "IWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x03004690, type: "U32", domain: "IWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030046A4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x030047C8, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x030047CA, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x030047CC, note: "Camera Z position", domain: "IWRAM"));
            Misc.Add("current_stat", new MemoryAddress(0x03002BE0, note: "For current stats", type: "U32", domain: "IWRAM"));

            /*
             * US version 
             */

            // Misc.Add("exp_table", new MemoryAddress(0x08ce3238));
        }
    }
}