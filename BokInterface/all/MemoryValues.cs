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

            // Current HP, ENE & form or character
            Django.Add("current_form", new DynamicMemoryValue("current_form", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["current_form"].Address));
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_ene"].Address));

            // Level, EXP & stat points to apply
            Django.Add("level", new DynamicMemoryValue("level", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["exp"].Address, _zoktaiAddresses.Django["exp"].Type));
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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_ene"].Address));
            Django.Add("current_trc", new DynamicMemoryValue("current_trc", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_trc"].Address));

            Django.Add("level", new DynamicMemoryValue("level", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["exp"].Address, _shinbokAddresses.Django["exp"].Type));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["stat_points"].Address));

            // Stats
            Django.Add("sum_base_cards_vit", new DynamicMemoryValue("sum_base_cards_vit", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_sum_base_cards_vit"].Address));
            Django.Add("equips_vit", new DynamicMemoryValue("equips_vit", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_equips_vit"].Address));

            Django.Add("sum_base_cards_spr", new DynamicMemoryValue("sum_base_cards_spr", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_sum_base_cards_spr"].Address));
            Django.Add("equips_spr", new DynamicMemoryValue("equips_spr", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_equips_spr"].Address));

            Django.Add("sum_base_cards_str", new DynamicMemoryValue("sum_base_cards_str", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_sum_base_cards_str"].Address));
            Django.Add("equips_str", new DynamicMemoryValue("equips_str", _shinbokAddresses.Misc["actor"].Address, _shinbokAddresses.Django["current_equips_str"].Address));

            // Will be applied when switching room
            Misc.Add("persistent_hp", new DynamicMemoryValue("persistent_hp", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_hp"].Address, _shinbokAddresses.Django["persistent_hp"].Type));
            Misc.Add("persistent_ene", new DynamicMemoryValue("persistent_ene", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_ene"].Address, _shinbokAddresses.Django["persistent_ene"].Type));
            Misc.Add("persistent_trc", new DynamicMemoryValue("persistent_trc", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_trc"].Address, _shinbokAddresses.Django["persistent_trc"].Type));

            Misc.Add("base_vit", new DynamicMemoryValue("base_vit", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_base_vit"].Address, _shinbokAddresses.Django["persistent_base_vit"].Type));
            Misc.Add("base_spr", new DynamicMemoryValue("base_spr", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_base_spr"].Address, _shinbokAddresses.Django["persistent_base_spr"].Type));
            Misc.Add("base_str", new DynamicMemoryValue("base_str", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_base_str"].Address, _shinbokAddresses.Django["persistent_base_str"].Type));

            Misc.Add("cards_vit", new DynamicMemoryValue("cards_vit", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_cards_vit"].Address, _shinbokAddresses.Django["persistent_cards_vit"].Type));
            Misc.Add("cards_spr", new DynamicMemoryValue("cards_spr", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_cards_spr"].Address, _shinbokAddresses.Django["persistent_cards_spr"].Type));
            Misc.Add("cards_str", new DynamicMemoryValue("cards_str", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["persistent_cards_str"].Address, _shinbokAddresses.Django["persistent_cards_str"].Type));

            // Solls
            Solls.Add("solar_station", new DynamicMemoryValue("solar_station", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Solls["solar_station"].Address, _shinbokAddresses.Solls["solar_station"].Type));
            Solls.Add("solar_bank", new DynamicMemoryValue("solar_bank", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Solls["solar_bank"].Address, _shinbokAddresses.Solls["solar_bank"].Type));

            // Solar Bike
            // Bike.Add("name", new DynamicMemoryValue("name", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["name"].Address));
            Bike.Add("points", new DynamicMemoryValue("points", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["points"].Address));
            Bike.Add("battle_matches", new DynamicMemoryValue("battle_matches", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["battle_matches"].Address));
            Bike.Add("battle_wins", new DynamicMemoryValue("battle_wins", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["battle_wins"].Address));
            Bike.Add("front", new DynamicMemoryValue("front", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["front"].Address));
            Bike.Add("tires", new DynamicMemoryValue("tires", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["tires"].Address));
            Bike.Add("body", new DynamicMemoryValue("body", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["body"].Address));
            Bike.Add("special", new DynamicMemoryValue("special", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["special"].Address));
            Bike.Add("color", new DynamicMemoryValue("color", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["color"].Address));
            Bike.Add("options", new DynamicMemoryValue("options", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Bike["options"].Address));

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
