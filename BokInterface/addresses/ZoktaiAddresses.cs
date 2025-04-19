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

            // Current form or character
            Django.Add("current_form", new MemoryAddress(0x250, note: "Current form or character", domain: "EWRAM"));

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

            /**
             * Inventory-related memory addresses
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = 1 + i;

                // Items & durability (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x70 + addressOffset, note: "Item slot", domain: "EWRAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0xD0 + addressOffset, note: "Item durability (for spoiling)", domain: "EWRAM"));

                // Key items (2 bytes)
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x130 + addressOffset, note: "Key item inventory slot", domain: "EWRAM"));

                // Accessories (2 bytes)
                Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x150 + addressOffset, note: "Accessory inventory slot", domain: "EWRAM"));

                // Weapons & parameters
                addressOffset = 0x1C * (uint)i;

                // Slot & bonus / malus (1 byte each)
                Inventory.Add("weapon_slot_" + slotNumber + "", new MemoryAddress(0x3D0 + addressOffset, note: "Weapon slot", domain: "EWRAM", type: "U8"));
                Inventory.Add("weapon_slot_" + slotNumber + "_bonus", new MemoryAddress(0x3D1 + addressOffset, note: "Bonus / malus (ex: +10 or -03)", domain: "EWRAM", type: "U8"));

                // Durability if bonus or malus is applied (2 bytes)
                Inventory.Add("weapon_slot_" + slotNumber + "_durability", new MemoryAddress(0x3D2 + addressOffset, note: "Durability (for the bonus / malus)", domain: "EWRAM"));

                // Forged by, simplified into 3 addresses (4 bytes each)
                _note = "ForgedBy Name (added when forging & used for the multiplayer shop)";
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_1", new MemoryAddress(0x3D4 + addressOffset, note: _note + "(part 1)", domain: "EWRAM", type: "U32"));
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_2", new MemoryAddress(0x3D8 + addressOffset, note: _note + "(part 2)", domain: "EWRAM", type: "U32"));
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_3", new MemoryAddress(0x3DC + addressOffset, note: _note + "(part 3)", domain: "EWRAM", type: "U32"));

                // Sp ability (4 bytes each, but we'll only read 1 to get a proper SP ability value)
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_1", new MemoryAddress(0x3E0 + addressOffset, note: "1st SP ability (of the current weapon itself)", domain: "EWRAM", type: "U8"));
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_2", new MemoryAddress(0x3E4 + addressOffset, note: "2nd SP ability (inherited through forging)", domain: "EWRAM", type: "U8"));
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_3", new MemoryAddress(0x3E8 + addressOffset, note: "3rd SP ability (inherited through forging)", domain: "EWRAM", type: "U8"));
            }

            // Magics (32 bytes, bitmask with 4 bytes values)
            Inventory.Add("magics", new MemoryAddress(0x54, note: "Magics", domain: "EWRAM", type: "U32"));
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