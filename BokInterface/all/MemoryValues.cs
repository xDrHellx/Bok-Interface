using System.Collections.Generic;

using BokInterface.Addresses;

namespace BokInterface.All {

    /// <summary>Class containing instances of memory values for the current game</summary>
    class MemoryValues {

        #region Properties

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

            ZoktaiAddresses memoryAddresses = new();

            // Current HP, ENE & form or character
            Django.Add("current_form", new DynamicMemoryValue("current_form", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["current_form"].Address));
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_ene"].Address));

            Django.Add("level", new DynamicMemoryValue("level", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["exp"].Address, memoryAddresses.Django["exp"].Type));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["stat_points_to_allocate"].Address));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_vit"].Address));
            Django.Add("spr", new DynamicMemoryValue("spr", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_spr"].Address));
            Django.Add("str", new DynamicMemoryValue("str", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_str"].Address));
            Django.Add("agi", new DynamicMemoryValue("agi", memoryAddresses.Misc["current_stat"].Address, memoryAddresses.Django["current_agi"].Address));

            // Kaamos
            Django.Add("kaamos", new DynamicMemoryValue("kaamos", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["kaamos_status"].Address));
            Sabata.Add("kaamos", new DynamicMemoryValue("kaamos", memoryAddresses.Misc["stat"].Address, memoryAddresses.Sabata["kaamos_status"].Address));

            // Skill
            Django.Add("sword_skill", new DynamicMemoryValue("sword_skill", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["sword_skill_exp"].Address));
            Django.Add("spear_skill", new DynamicMemoryValue("spear_skill", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["spear_skill_exp"].Address));
            Django.Add("hammer_skill", new DynamicMemoryValue("hammer_skill", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["hammer_skill_exp"].Address));
            Django.Add("fists_skill", new DynamicMemoryValue("fists_skill", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["fists_skill_exp"].Address));
            Django.Add("gun_skill", new DynamicMemoryValue("gun_skill", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["gun_skill_exp"].Address));

            // Stats that will be applied when switching room
            Misc.Add("vit", new DynamicMemoryValue("vit", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_vit"].Address));
            Misc.Add("spr", new DynamicMemoryValue("spr", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_spr"].Address));
            Misc.Add("str", new DynamicMemoryValue("str", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_str"].Address));
            Misc.Add("agi", new DynamicMemoryValue("agi", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_agi"].Address));

            // U32
            U32.Add("total_exp_until_next_level", memoryAddresses.Django["total_exp_until_next_level"]);

            /**
             * Inventory-related
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = i + 1;

                // Items, key items & accessories
                Inventory.Add("slot" + slotNumber + "_item", new DynamicMemoryValue("slot" + slotNumber + "_item", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_durability", new DynamicMemoryValue("slot" + slotNumber + "_durability", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["item_slot_durability_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_key_item", new DynamicMemoryValue("slot" + slotNumber + "_key_item", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["key_item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_accessory", new DynamicMemoryValue("slot" + slotNumber + "_accessory", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["accessory_slot_" + slotNumber].Address));

                // Weapons
                Inventory.Add("slot" + slotNumber + "_weapon", new DynamicMemoryValue("slot" + slotNumber + "_weapon", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_bonus", new DynamicMemoryValue("slot" + slotNumber + "_weapon_bonus", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_bonus"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_bonus"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_durability", new DynamicMemoryValue("slot" + slotNumber + "_weapon_durability", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_1", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_1"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_2", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_2"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_2"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_forgedBy_3", new DynamicMemoryValue("slot" + slotNumber + "_weapon_forgedBy_3", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_3"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_forgedBy_3"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_1", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_2", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_3", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_3", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_3"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_3"].Type));
            }

            // Magics
            Inventory.Add("magics", new DynamicMemoryValue("magics", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["magics"].Address, type: memoryAddresses.Inventory["magics"].Type));
        }

        private void InitializeShinbokList() {

            ShinbokAddresses memoryAddresses = new();

            Django.Add("current_hp", new DynamicMemoryValue("current_hp", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_ene"].Address));
            Django.Add("current_trc", new DynamicMemoryValue("current_trc", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_trc"].Address));

            Django.Add("level", new DynamicMemoryValue("level", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["exp"].Address, memoryAddresses.Django["exp"].Type));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["stat_points"].Address));

            // Stats
            Django.Add("sum_base_cards_vit", new DynamicMemoryValue("sum_base_cards_vit", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_sum_base_cards_vit"].Address));
            Django.Add("equips_vit", new DynamicMemoryValue("equips_vit", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_equips_vit"].Address));

            Django.Add("sum_base_cards_spr", new DynamicMemoryValue("sum_base_cards_spr", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_sum_base_cards_spr"].Address));
            Django.Add("equips_spr", new DynamicMemoryValue("equips_spr", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_equips_spr"].Address));

            Django.Add("sum_base_cards_str", new DynamicMemoryValue("sum_base_cards_str", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_sum_base_cards_str"].Address));
            Django.Add("equips_str", new DynamicMemoryValue("equips_str", memoryAddresses.Misc["actor"].Address, memoryAddresses.Django["current_equips_str"].Address));

            // Will be applied when switching room
            Misc.Add("persistent_hp", new DynamicMemoryValue("persistent_hp", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_hp"].Address, memoryAddresses.Django["persistent_hp"].Type));
            Misc.Add("persistent_ene", new DynamicMemoryValue("persistent_ene", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_ene"].Address, memoryAddresses.Django["persistent_ene"].Type));
            Misc.Add("persistent_trc", new DynamicMemoryValue("persistent_trc", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_trc"].Address, memoryAddresses.Django["persistent_trc"].Type));

            Misc.Add("base_vit", new DynamicMemoryValue("base_vit", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_base_vit"].Address, memoryAddresses.Django["persistent_base_vit"].Type));
            Misc.Add("base_spr", new DynamicMemoryValue("base_spr", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_base_spr"].Address, memoryAddresses.Django["persistent_base_spr"].Type));
            Misc.Add("base_str", new DynamicMemoryValue("base_str", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_base_str"].Address, memoryAddresses.Django["persistent_base_str"].Type));

            Misc.Add("cards_vit", new DynamicMemoryValue("cards_vit", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_cards_vit"].Address, memoryAddresses.Django["persistent_cards_vit"].Type));
            Misc.Add("cards_spr", new DynamicMemoryValue("cards_spr", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_cards_spr"].Address, memoryAddresses.Django["persistent_cards_spr"].Type));
            Misc.Add("cards_str", new DynamicMemoryValue("cards_str", memoryAddresses.Misc["stat"].Address, memoryAddresses.Django["persistent_cards_str"].Address, memoryAddresses.Django["persistent_cards_str"].Type));

            // Solls
            Solls.Add("solar_station", new DynamicMemoryValue("solar_station", memoryAddresses.Misc["stat"].Address, memoryAddresses.Solls["solar_station"].Address, memoryAddresses.Solls["solar_station"].Type));
            Solls.Add("solar_bank", new DynamicMemoryValue("solar_bank", memoryAddresses.Misc["stat"].Address, memoryAddresses.Solls["solar_bank"].Address, memoryAddresses.Solls["solar_bank"].Type));

            // Solar Bike
            // Bike.Add("name", new DynamicMemoryValue("name", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["name"].Address));
            Bike.Add("points", new DynamicMemoryValue("points", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["points"].Address));
            Bike.Add("battle_matches", new DynamicMemoryValue("battle_matches", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["battle_matches"].Address));
            Bike.Add("battle_wins", new DynamicMemoryValue("battle_wins", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["battle_wins"].Address));
            Bike.Add("front", new DynamicMemoryValue("front", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["front"].Address));
            Bike.Add("tires", new DynamicMemoryValue("tires", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["tires"].Address));
            Bike.Add("body", new DynamicMemoryValue("body", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["body"].Address));
            Bike.Add("special", new DynamicMemoryValue("special", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["special"].Address));
            Bike.Add("color", new DynamicMemoryValue("color", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["color"].Address));
            Bike.Add("options", new DynamicMemoryValue("options", memoryAddresses.Misc["stat"].Address, memoryAddresses.Bike["options"].Address));

            /**
             * Items, key items, accessories & weapons inventories
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = i + 1;

                // Items, durability, key items & accessory slots
                Inventory.Add("slot" + slotNumber + "_item", new DynamicMemoryValue("slot" + slotNumber + "_item", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_durability", new DynamicMemoryValue("slot" + slotNumber + "_durability", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["item_slot_durability_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_key_item", new DynamicMemoryValue("slot" + slotNumber + "_key_item", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["key_item_slot_" + slotNumber].Address));
                Inventory.Add("slot" + slotNumber + "_accessory", new DynamicMemoryValue("slot" + slotNumber + "_accessory", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["accessory_slot_" + slotNumber].Address));

                // Weapon slots & properties
                Inventory.Add("slot" + slotNumber + "_weapon", new DynamicMemoryValue("slot" + slotNumber + "_weapon", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_durability", new DynamicMemoryValue("slot" + slotNumber + "_weapon_durability", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_durability"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_1", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_1", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_1"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_sp_ability_2", new DynamicMemoryValue("slot" + slotNumber + "_weapon_sp_ability_2", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_sp_ability_2"].Type));

                Inventory.Add("slot" + slotNumber + "_weapon_refine", new DynamicMemoryValue("slot" + slotNumber + "_weapon_refine", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_refine"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_refine"].Type));
                Inventory.Add("slot" + slotNumber + "_weapon_pattern", new DynamicMemoryValue("slot" + slotNumber + "_weapon_pattern", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_pattern"].Address, type: memoryAddresses.Inventory["weapon_slot_" + slotNumber + "_pattern"].Type));
            }

            // Gun lenses & frames inventories
            for (int i = 0; i < 8; i++) {
                int slotNumber = i + 1;
                Inventory.Add("slot" + slotNumber + "_gun_lens", new DynamicMemoryValue("slot" + slotNumber + "_gun_lens", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["gun_lens_slot_" + slotNumber].Address));
            }

            for (int i = 0; i < 12; i++) {
                int slotNumber = i + 1;
                Inventory.Add("slot" + slotNumber + "_gun_frame", new DynamicMemoryValue("slot" + slotNumber + "_gun_frame", memoryAddresses.Misc["stat"].Address, memoryAddresses.Inventory["gun_frame_slot_" + slotNumber].Address));
            }
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
