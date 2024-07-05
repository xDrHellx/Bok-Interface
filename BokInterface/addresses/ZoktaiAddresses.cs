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
            InitWeaponInventoryAddresses();
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
        }

        protected void InitWeaponInventoryAddresses() {

            // Slot 1
            _note = "Weapon inventory slot";
            Inventory.Add("weapon_slot_1", new MemoryAddress(0x3D0, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_bonus", new MemoryAddress(0x3D1, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_durability", new MemoryAddress(0x3D2, domain: "EWRAM"));

            // Forged by, simplified into 3 addresses (4 bytes each)
            Inventory.Add("weapon_slot_1_forgedBy_1", new MemoryAddress(0x3D4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_forgedBy_2", new MemoryAddress(0x3D8, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_forgedBy_3", new MemoryAddress(0x3DC, domain: "EWRAM"));

            // Sp ability (4 bytes each)
            Inventory.Add("weapon_slot_1_sp_ability_1", new MemoryAddress(0x3E0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_sp_ability_2", new MemoryAddress(0x3E4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_1_sp_ability_3", new MemoryAddress(0x3E8, domain: "EWRAM"));

            // Slot 2
            Inventory.Add("weapon_slot_2", new MemoryAddress(0x3EC, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_bonus", new MemoryAddress(0x3ED, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_durability", new MemoryAddress(0x3EE, domain: "EWRAM"));

            Inventory.Add("weapon_slot_2_forgedBy_1", new MemoryAddress(0x3F0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_forgedBy_2", new MemoryAddress(0x3F4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_forgedBy_3", new MemoryAddress(0x3F8, domain: "EWRAM"));

            Inventory.Add("weapon_slot_2_sp_ability_1", new MemoryAddress(0x3FC, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_sp_ability_2", new MemoryAddress(0x400, domain: "EWRAM"));
            Inventory.Add("weapon_slot_2_sp_ability_3", new MemoryAddress(0x404, domain: "EWRAM"));

            // Slot 3
            Inventory.Add("weapon_slot_3", new MemoryAddress(0x408, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_bonus", new MemoryAddress(0x409, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_durability", new MemoryAddress(0x40A, domain: "EWRAM"));

            Inventory.Add("weapon_slot_3_forgedBy_1", new MemoryAddress(0x40C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_forgedBy_2", new MemoryAddress(0x410, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_forgedBy_3", new MemoryAddress(0x414, domain: "EWRAM"));

            Inventory.Add("weapon_slot_3_sp_ability_1", new MemoryAddress(0x418, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_sp_ability_2", new MemoryAddress(0x41C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_3_sp_ability_3", new MemoryAddress(0x420, domain: "EWRAM"));

            // Slot 4
            Inventory.Add("weapon_slot_4", new MemoryAddress(0x424, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_bonus", new MemoryAddress(0x425, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_durability", new MemoryAddress(0x426, domain: "EWRAM"));

            Inventory.Add("weapon_slot_4_forgedBy_1", new MemoryAddress(0x428, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_forgedBy_2", new MemoryAddress(0x42C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_forgedBy_3", new MemoryAddress(0x430, domain: "EWRAM"));

            Inventory.Add("weapon_slot_4_sp_ability_1", new MemoryAddress(0x434, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_sp_ability_2", new MemoryAddress(0x438, domain: "EWRAM"));
            Inventory.Add("weapon_slot_4_sp_ability_3", new MemoryAddress(0x43C, domain: "EWRAM"));

            // Slot 5
            Inventory.Add("weapon_slot_5", new MemoryAddress(0x440, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_bonus", new MemoryAddress(0x441, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_durability", new MemoryAddress(0x442, domain: "EWRAM"));

            Inventory.Add("weapon_slot_5_forgedBy_1", new MemoryAddress(0x444, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_forgedBy_2", new MemoryAddress(0x448, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_forgedBy_3", new MemoryAddress(0x44C, domain: "EWRAM"));

            Inventory.Add("weapon_slot_5_sp_ability_1", new MemoryAddress(0x450, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_sp_ability_2", new MemoryAddress(0x454, domain: "EWRAM"));
            Inventory.Add("weapon_slot_5_sp_ability_3", new MemoryAddress(0x458, domain: "EWRAM"));

            // Slot 6
            Inventory.Add("weapon_slot_6", new MemoryAddress(0x45C, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_bonus", new MemoryAddress(0x45D, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_durability", new MemoryAddress(0x45E, domain: "EWRAM"));

            Inventory.Add("weapon_slot_6_forgedBy_1", new MemoryAddress(0x460, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_forgedBy_2", new MemoryAddress(0x464, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_forgedBy_3", new MemoryAddress(0x468, domain: "EWRAM"));

            Inventory.Add("weapon_slot_6_sp_ability_1", new MemoryAddress(0x46C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_sp_ability_2", new MemoryAddress(0x470, domain: "EWRAM"));
            Inventory.Add("weapon_slot_6_sp_ability_3", new MemoryAddress(0x474, domain: "EWRAM"));

            // Slot 7
            Inventory.Add("weapon_slot_7", new MemoryAddress(0x478, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_bonus", new MemoryAddress(0x479, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_durability", new MemoryAddress(0x47A, domain: "EWRAM"));

            Inventory.Add("weapon_slot_7_forgedBy_1", new MemoryAddress(0x47C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_forgedBy_2", new MemoryAddress(0x480, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_forgedBy_3", new MemoryAddress(0x484, domain: "EWRAM"));

            Inventory.Add("weapon_slot_7_sp_ability_1", new MemoryAddress(0x488, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_sp_ability_2", new MemoryAddress(0x48C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_7_sp_ability_3", new MemoryAddress(0x490, domain: "EWRAM"));

            // Slot 8
            Inventory.Add("weapon_slot_8", new MemoryAddress(0x494, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_bonus", new MemoryAddress(0x495, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_durability", new MemoryAddress(0x496, domain: "EWRAM"));

            Inventory.Add("weapon_slot_8_forgedBy_1", new MemoryAddress(0x498, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_forgedBy_2", new MemoryAddress(0x49C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_forgedBy_3", new MemoryAddress(0x4A0, domain: "EWRAM"));

            Inventory.Add("weapon_slot_8_sp_ability_1", new MemoryAddress(0x4A4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_sp_ability_2", new MemoryAddress(0x4A8, domain: "EWRAM"));
            Inventory.Add("weapon_slot_8_sp_ability_3", new MemoryAddress(0x4AC, domain: "EWRAM"));

            // Slot 9
            Inventory.Add("weapon_slot_9", new MemoryAddress(0x4B0, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_bonus", new MemoryAddress(0x4B1, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_durability", new MemoryAddress(0x4B2, domain: "EWRAM"));

            Inventory.Add("weapon_slot_9_forgedBy_1", new MemoryAddress(0x4B4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_forgedBy_2", new MemoryAddress(0x4B8, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_forgedBy_3", new MemoryAddress(0x4BC, domain: "EWRAM"));

            Inventory.Add("weapon_slot_9_sp_ability_1", new MemoryAddress(0x4C0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_sp_ability_2", new MemoryAddress(0x4C4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_9_sp_ability_3", new MemoryAddress(0x4C8, domain: "EWRAM"));

            // Slot 10
            Inventory.Add("weapon_slot_10", new MemoryAddress(0x4CC, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_bonus", new MemoryAddress(0x4CD, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_durability", new MemoryAddress(0x4CE, domain: "EWRAM"));

            Inventory.Add("weapon_slot_10_forgedBy_1", new MemoryAddress(0x4D0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_forgedBy_2", new MemoryAddress(0x4D4, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_forgedBy_3", new MemoryAddress(0x4D8, domain: "EWRAM"));

            Inventory.Add("weapon_slot_10_sp_ability_1", new MemoryAddress(0x4DC, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_sp_ability_2", new MemoryAddress(0x4E0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_10_sp_ability_3", new MemoryAddress(0x4E4, domain: "EWRAM"));

            // Slot 11
            Inventory.Add("weapon_slot_11", new MemoryAddress(0x4E8, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_bonus", new MemoryAddress(0x4E9, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_durability", new MemoryAddress(0x4EA, domain: "EWRAM"));

            Inventory.Add("weapon_slot_11_forgedBy_1", new MemoryAddress(0x4EC, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_forgedBy_2", new MemoryAddress(0x4F0, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_forgedBy_3", new MemoryAddress(0x4F4, domain: "EWRAM"));

            Inventory.Add("weapon_slot_11_sp_ability_1", new MemoryAddress(0x4F8, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_sp_ability_2", new MemoryAddress(0x4FC, domain: "EWRAM"));
            Inventory.Add("weapon_slot_11_sp_ability_3", new MemoryAddress(0x500, domain: "EWRAM"));

            // Slot 12
            Inventory.Add("weapon_slot_12", new MemoryAddress(0x504, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_bonus", new MemoryAddress(0x505, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_durability", new MemoryAddress(0x506, domain: "EWRAM"));

            Inventory.Add("weapon_slot_12_forgedBy_1", new MemoryAddress(0x508, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_forgedBy_2", new MemoryAddress(0x50C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_forgedBy_3", new MemoryAddress(0x510, domain: "EWRAM"));

            Inventory.Add("weapon_slot_12_sp_ability_1", new MemoryAddress(0x514, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_sp_ability_2", new MemoryAddress(0x518, domain: "EWRAM"));
            Inventory.Add("weapon_slot_12_sp_ability_3", new MemoryAddress(0x51C, domain: "EWRAM"));

            // Slot 13
            Inventory.Add("weapon_slot_13", new MemoryAddress(0x520, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_bonus", new MemoryAddress(0x521, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_durability", new MemoryAddress(0x522, domain: "EWRAM"));

            Inventory.Add("weapon_slot_13_forgedBy_1", new MemoryAddress(0x524, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_forgedBy_2", new MemoryAddress(0x528, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_forgedBy_3", new MemoryAddress(0x52C, domain: "EWRAM"));

            Inventory.Add("weapon_slot_13_sp_ability_1", new MemoryAddress(0x530, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_sp_ability_2", new MemoryAddress(0x534, domain: "EWRAM"));
            Inventory.Add("weapon_slot_13_sp_ability_3", new MemoryAddress(0x538, domain: "EWRAM"));

            // Slot 14
            Inventory.Add("weapon_slot_14", new MemoryAddress(0x53C, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_bonus", new MemoryAddress(0x53D, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_durability", new MemoryAddress(0x53E, domain: "EWRAM"));

            Inventory.Add("weapon_slot_14_forgedBy_1", new MemoryAddress(0x540, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_forgedBy_2", new MemoryAddress(0x544, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_forgedBy_3", new MemoryAddress(0x548, domain: "EWRAM"));

            Inventory.Add("weapon_slot_14_sp_ability_1", new MemoryAddress(0x54C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_sp_ability_2", new MemoryAddress(0x550, domain: "EWRAM"));
            Inventory.Add("weapon_slot_14_sp_ability_3", new MemoryAddress(0x554, domain: "EWRAM"));

            // Slot 15
            Inventory.Add("weapon_slot_15", new MemoryAddress(0x558, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_bonus", new MemoryAddress(0x559, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_durability", new MemoryAddress(0x55A, domain: "EWRAM"));

            Inventory.Add("weapon_slot_15_forgedBy_1", new MemoryAddress(0x55C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_forgedBy_2", new MemoryAddress(0x560, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_forgedBy_3", new MemoryAddress(0x564, domain: "EWRAM"));

            Inventory.Add("weapon_slot_15_sp_ability_1", new MemoryAddress(0x568, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_sp_ability_2", new MemoryAddress(0x56C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_15_sp_ability_3", new MemoryAddress(0x570, domain: "EWRAM"));

            // Slot 16
            Inventory.Add("weapon_slot_16", new MemoryAddress(0x574, note: _note, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_bonus", new MemoryAddress(0x575, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_durability", new MemoryAddress(0x576, domain: "EWRAM"));

            Inventory.Add("weapon_slot_16_forgedBy_1", new MemoryAddress(0x578, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_forgedBy_2", new MemoryAddress(0x57C, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_forgedBy_3", new MemoryAddress(0x580, domain: "EWRAM"));

            Inventory.Add("weapon_slot_16_sp_ability_1", new MemoryAddress(0x584, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_sp_ability_2", new MemoryAddress(0x588, domain: "EWRAM"));
            Inventory.Add("weapon_slot_16_sp_ability_3", new MemoryAddress(0x58C, domain: "EWRAM"));
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