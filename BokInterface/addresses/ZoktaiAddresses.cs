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

        /// <summary>Solls-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Solls = new Dictionary<string, MemoryAddress>();

        /// <summary>
        ///     <para>Misc memory addresses</para>
        ///     <para>
        ///         These are used in combination with other memory addresses to get / set values that are "dynamic."<br/>
        ///         <i>For example the memory address for Django's current HP is different based on which "room sections" he is in.</i>
        ///     </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        /// <summary>Downloadable events / JoySpots related memory addresses</summary>
        public IDictionary<string, MemoryAddress> JoySpots = new Dictionary<string, MemoryAddress>();

        /// <summary>Note for MemoryAddress instances (for less repetition)</summary>
        private string _note = "";

        public ZoktaiAddresses() {
            InitPlayableCharactersAddresses();
            InitInventoryAddresses();
            InitSollsAddresses();
            InitMiscAddresses();
            InitJoySpotsAddresses();
        }

        private void InitPlayableCharactersAddresses() {

            // Position coordinates
            Django.Add("x_position", new MemoryAddress(0x30, "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x34, "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x32, "Django Z position", domain: "EWRAM"));

            // Current stats
            _note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Django.Add("current_hp", new MemoryAddress(0x364, _note, domain: "EWRAM"));
            Django.Add("current_ene", new MemoryAddress(0x368, _note, domain: "EWRAM"));
            Django.Add("current_vit", new MemoryAddress(0x35C, _note, domain: "EWRAM"));
            Django.Add("current_spr", new MemoryAddress(0x35E, _note, domain: "EWRAM"));
            Django.Add("current_str", new MemoryAddress(0x360, _note, domain: "EWRAM"));
            Django.Add("current_agi", new MemoryAddress(0x362, _note, domain: "EWRAM"));

            /**
             * Persistent stats (used on screen transitions & save data)
             * Note : For some stats, "current" can be 1 higher than "persistent", unsure why
             */
            _note = "Also corresponds to values from Save Data";
            Django.Add("persistent_hp", new MemoryAddress(0x28, _note, domain: "EWRAM"));
            Django.Add("persistent_ene", new MemoryAddress(0x2C, _note, domain: "EWRAM"));
            Django.Add("persistent_vit", new MemoryAddress(0x18, _note, domain: "EWRAM"));
            Django.Add("persistent_spr", new MemoryAddress(0x1A, _note, domain: "EWRAM"));
            Django.Add("persistent_str", new MemoryAddress(0x1C, _note, domain: "EWRAM"));
            Django.Add("persistent_agi", new MemoryAddress(0x1E, _note, domain: "EWRAM"));

            // EXP & level
            Django.Add("level", new MemoryAddress(0x40, domain: "EWRAM"));
            Django.Add("exp", new MemoryAddress(0x50, type: "U32", domain: "EWRAM"));
            Django.Add("total_exp_until_next_level", new MemoryAddress(0x2464, type: "U32", domain: "EWRAM"));

            // Skill
            _note = "100 skill exp = 1 lvl";
            Django.Add("sword_skill_exp", new MemoryAddress(0x46, _note, domain: "EWRAM"));
            Django.Add("spear_skill_exp", new MemoryAddress(0x48, _note, domain: "EWRAM"));
            Django.Add("hammer_skill_exp", new MemoryAddress(0x4A, _note, domain: "EWRAM"));
            Django.Add("fists_skill_exp", new MemoryAddress(0x4C, _note, domain: "EWRAM"));
            Django.Add("gun_skill_exp", new MemoryAddress(0x4E, _note, domain: "EWRAM"));

            // Stat points
            Django.Add("stat_points_to_allocate", new MemoryAddress(0x42, domain: "EWRAM"));

            // Status effects
            _note = "Kaamos status is applied when this value is > 35999 & it rains";
            Django.Add("kaamos_status", new MemoryAddress(0x2C8, domain: "EWRAM"));
            Django.Add("kaamos_progress", new MemoryAddress(0x2C0, _note, domain: "EWRAM"));

            // Sabata
            Sabata.Add("kaamos_status", new MemoryAddress(0x02CE, domain: "EWRAM"));
            Sabata.Add("kaamos_progress", new MemoryAddress(0x2C4, _note, domain: "EWRAM"));

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

        private void InitInventoryAddresses() {
            _note = "ForgedBy Name (added when forging & used for the multiplayer shop)";

            for (int i = 0; i < 16; i++) {
                int slotNumber = 1 + i;

                // Items, durability, key items & accessories (2 bytes each)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0x70 + addressOffset, "Item slot", domain: "EWRAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0xD0 + addressOffset, "Item durability (for spoiling)", domain: "EWRAM"));
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x130 + addressOffset, "Key item inventory slot", domain: "EWRAM"));
                Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x150 + addressOffset, "Accessory inventory slot", domain: "EWRAM"));

                // Weapons & parameters
                addressOffset = 0x1C * (uint)i;

                // Slot & bonus / malus (1 byte each)
                Inventory.Add("weapon_slot_" + slotNumber + "", new MemoryAddress(0x3D0 + addressOffset, "Weapon slot", "U8", domain: "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_bonus", new MemoryAddress(0x3D1 + addressOffset, "Bonus / malus (ex: +10 or -03)", "U8", "EWRAM"));

                // Durability if bonus or malus is applied (2 bytes)
                Inventory.Add("weapon_slot_" + slotNumber + "_durability", new MemoryAddress(0x3D2 + addressOffset, "Durability (for the bonus / malus)", domain: "EWRAM"));

                // Forged by, simplified into 3 addresses (4 bytes each)
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_1", new MemoryAddress(0x3D4 + addressOffset, _note + "(part 1)", "U32", "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_2", new MemoryAddress(0x3D8 + addressOffset, _note + "(part 2)", "U32", "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_forgedBy_3", new MemoryAddress(0x3DC + addressOffset, _note + "(part 3)", "U32", "EWRAM"));

                // Sp ability (4 bytes each, but we'll only read 1 to get a proper SP ability value)
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_1", new MemoryAddress(0x3E0 + addressOffset, "1st SP ability (of the current weapon itself)", "U8", "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_2", new MemoryAddress(0x3E4 + addressOffset, "2nd SP ability (inherited through forging)", "U8", "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_3", new MemoryAddress(0x3E8 + addressOffset, "3rd SP ability (inherited through forging)", "U8", "EWRAM"));
            }

            // Magics (32 bytes, bitmask with 4 bytes values)
            Inventory.Add("magics", new MemoryAddress(0x54, "Magics", "U32", "EWRAM"));
        }

        private void InitMiscAddresses() {
            Misc.Add("stat", new MemoryAddress(0x030046A0, "For persistent stats & inventory", "U32", "IWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x03004698, "Story progress & dungeon states", "U32", "IWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x03004690, type: "U32", domain: "IWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030046A4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x030047C8, "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x030047CA, "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x030047CC, "Camera Z position", domain: "IWRAM"));
            Misc.Add("current_stat", new MemoryAddress(0x03002BE0, "For current stats", "U32", "IWRAM"));
            Misc.Add("rng_index", new MemoryAddress(0x030046B8, type: "U32", domain: "IWRAM"));
            Misc.Add("rtc_date", new MemoryAddress(0x030047E0, "Binary-code decimal (yyyymmdd format)", "U32", "IWRAM"));
            Misc.Add("rtc_hours", new MemoryAddress(0x030047E4, type: "U8", domain: "IWRAM"));
            Misc.Add("rtc_minutes", new MemoryAddress(0x030047E5, type: "U8", domain: "IWRAM"));
            Misc.Add("rtc_seconds", new MemoryAddress(0x030047E6, type: "U8", domain: "IWRAM"));
            Misc.Add("rtc_frames", new MemoryAddress(0x030047E7, type: "U8", domain: "IWRAM"));
            Misc.Add("igt_frame_counter", new MemoryAddress(0x2b4, "In-Game Time frame counter for the current save file", "U32", "EWRAM"));

            // US version
            // Misc.Add("exp_table", new MemoryAddress(0x08CE3238));
        }

        private void InitSollsAddresses() {
            Solls.Add("solar_station", new MemoryAddress(0x3BC, "Solar station balance", "U32", "EWRAM"));
            Solls.Add("solar_bank", new MemoryAddress(0x910, "Solar bank balance", "U32", "EWRAM"));
            Solls.Add("dark_loans", new MemoryAddress(0x1C4, "Dark loans", "EWRAM"));
            Solls.Add("interest_rate", new MemoryAddress(0x23A, "Solar Bank interest rate", domain: "EWRAM"));
        }

        private void InitJoySpotsAddresses() {
            // Note: These events could be activated from "Joy Spots" in Japan with the wireless adapter
            JoySpots.Add("blindbox_lvl_3", new MemoryAddress(0x030016D8, "Blindbox Lv. 3 from ??? (set value to 0x8E67 to activate)", domain: "System Bus"));
            JoySpots.Add("blindbox_lvl_4", new MemoryAddress(0x030016DA, "Blindbox Lv. 4 from ??? (set value to 0x8FAA to activate)", domain: "System Bus"));
            JoySpots.Add("blindbox_lvl_5_valentine_day", new MemoryAddress(0x030016DC, "Blindbox Lv. 5 from ??? & Valentine Day (only on February 14th, set value to 0x90E0 to activate)", domain: "System Bus"));
            JoySpots.Add("star_piece", new MemoryAddress(0x030016DE, "Star Piece from ??? (set value to 0x8FD6 to activate)", domain: "System Bus"));
        }
    }
}
