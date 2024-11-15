using System.Collections.Generic;

using BokInterface.Addresses;

namespace BokInterface.All {

    /// <summary>Class containing instances of memory values for the current game</summary>
    class MemoryValues {

        #region Properties

        private readonly ZoktaiAddresses _zoktaiAddresses = new();
        private readonly ShinbokAddresses _shinbokAddresses = new();

        /// <summary>Django-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Django = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>Sabata-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Sabata = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>Solls-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Solls = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>Inventory-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Inventory = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>Bike-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Bike = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>Misc memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Misc = new Dictionary<string, DynamicMemoryValue>();
        /// <summary>U16 memory values</summary>
        public IDictionary<string, MemoryAddress> U16 = new Dictionary<string, MemoryAddress>();
        /// <summary>U32 memory values</summary>
        public IDictionary<string, MemoryAddress> U32 = new Dictionary<string, MemoryAddress>();

        #endregion

        /// <summary>Constructor</summary>
        /// <param name="shorterGameName">Shortened game name (used for setting the lists containing the memory values instances)</param>
        public MemoryValues(string shorterGameName) {

            ClearLists();

            switch (shorterGameName) {
                case "Boktai":
                    InitializeBoktaiList();
                    break;
                case "Zoktai":
                    InitializeZoktaiList();
                    break;
                case "Shinbok":
                    InitializeShinbokList();
                    break;
                case "LunarKnights":
                    InitializeLunarKnightsList();
                    break;
                default:
                    break;
            }
        }

        /// <summary>Clears all lists</summary>
        private void ClearLists() {
            Django.Clear();
            Sabata.Clear();
            Solls.Clear();
            Bike.Clear();
            Misc.Clear();
        }

        private void InitializeBoktaiList() {

        }

        private void InitializeZoktaiList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_ene"].Address));

            Django.Add("level", new DynamicMemoryValue("level", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["exp"].Address, "U32"));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["stat_points_to_allocate"].Address));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_vit"].Address));
            Django.Add("spr", new DynamicMemoryValue("spr", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_spr"].Address));
            Django.Add("str", new DynamicMemoryValue("str", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_str"].Address));
            Django.Add("agi", new DynamicMemoryValue("agi", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_agi"].Address));

            // Kaamos
            Django.Add("kaamos", new DynamicMemoryValue("kaamos", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["kaamos_status"].Address));
            Sabata.Add("kaamos", new DynamicMemoryValue("kaamos", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Sabata["kaamos_status"].Address));

            // Skill
            Django.Add("sword_skill", new DynamicMemoryValue("sword_skill", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["sword_skill_exp"].Address));
            Django.Add("spear_skill", new DynamicMemoryValue("spear_skill", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["spear_skill_exp"].Address));
            Django.Add("hammer_skill", new DynamicMemoryValue("hammer_skill", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["hammer_skill_exp"].Address));
            Django.Add("fists_skill", new DynamicMemoryValue("fists_skill", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["fists_skill_exp"].Address));
            Django.Add("gun_skill", new DynamicMemoryValue("gun_skill", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["gun_skill_exp"].Address));

            // Stats that will be applied when switching room
            Misc.Add("vit", new DynamicMemoryValue("vit", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["persistent_vit"].Address));
            Misc.Add("spr", new DynamicMemoryValue("spr", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["persistent_spr"].Address));
            Misc.Add("str", new DynamicMemoryValue("str", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["persistent_str"].Address));
            Misc.Add("agi", new DynamicMemoryValue("agi", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["persistent_agi"].Address));

            // U32
            U32.Add("total_exp_until_next_level", _zoktaiAddresses.Django["total_exp_until_next_level"]);

            /**
             * Inventory-related
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = i + 1;

                // Items, key items & accessories
                Inventory.Add("slot" + slotNumber + "_item", new DynamicMemoryValue("slot" + slotNumber + "_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_durability", new DynamicMemoryValue("slot" + slotNumber + "_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_key_item", new DynamicMemoryValue("slot" + slotNumber + "_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_accessory", new DynamicMemoryValue("slot" + slotNumber + "_accessory", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["accessory_slot_" + slotNumber].Address));

                // Weapons
                Inventory.Add("slot" + slotNumber + "_weapon", new DynamicMemoryValue("slot" + slotNumber + "_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_bonus", new DynamicMemoryValue("slot" + slotNumber + "_weapon_bonus", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_bonus"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_bonus"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_durability", new DynamicMemoryValue("slot" + slotNumber + "_weapon_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_1", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_1"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_2", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_2"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_2"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_3", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_3", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_3"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_3"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_1", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_2", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_3", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_3", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_3"].Address, type: _zoktaiAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_3"].Type));
            }

            // Magics
            Inventory.Add("magics", new DynamicMemoryValue("magics", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["magics"].Address, type: _zoktaiAddresses.Inventory["magics"].Type));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _shinbokAddresses.Misc["room"].Address, _shinbokAddresses.Django["hp"].Address));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_vit"].Address));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_spr"].Address));
            Django.Add("base_str", new DynamicMemoryValue("base_str", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_str"].Address));

            /**
             * Inventory-related
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = i + 1;

                // Items, durability, key items & accessory slots
                Inventory.Add("slot" + slotNumber + "_item", new DynamicMemoryValue("slot" + slotNumber + "_item", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_durability", new DynamicMemoryValue("slot" + slotNumber + "_durability", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["item_slot_durability_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_key_item", new DynamicMemoryValue("slot" + slotNumber + "_key_item", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["key_item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_accessory", new DynamicMemoryValue("slot" + slotNumber + "_accessory", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["accessory_slot_" + slotNumber].Address));

                // Weapon slots & properties
                Inventory.Add("slot" + slotNumber + "_weapon", new DynamicMemoryValue("slot" + slotNumber + "_weapon", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_durability", new DynamicMemoryValue("slot" + slotNumber + "_weapon_durability", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_1", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_2", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_refine", new DynamicMemoryValue("slot" + slotNumber + "_weapon_refine", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_refine"].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_refine"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_pattern", new DynamicMemoryValue("slot" + slotNumber + "_weapon_pattern", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_pattern"].Address, type: _shinbokAddresses.Inventory["weapon_slot_" + slotNumber + "_pattern"].Type));
            }
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
