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

        private void ClearLists() {
            Django.Clear();
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
            Django.Add("exp", new DynamicMemoryValue("exp", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["exp"].Address, "U32"));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Django["stat_points_to_allocate"].Address));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_vit"].Address));
            Django.Add("spr", new DynamicMemoryValue("spr", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_spr"].Address));
            Django.Add("str", new DynamicMemoryValue("str", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_str"].Address));
            Django.Add("agi", new DynamicMemoryValue("agi", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_agi"].Address));

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

            // Items inventory slots
            Inventory.Add("slot1_item", new DynamicMemoryValue("slot1_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_1"].Address));
            Inventory.Add("slot2_item", new DynamicMemoryValue("slot2_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_2"].Address));
            Inventory.Add("slot3_item", new DynamicMemoryValue("slot3_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_3"].Address));
            Inventory.Add("slot4_item", new DynamicMemoryValue("slot4_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_4"].Address));
            Inventory.Add("slot5_item", new DynamicMemoryValue("slot5_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_5"].Address));
            Inventory.Add("slot6_item", new DynamicMemoryValue("slot6_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_6"].Address));
            Inventory.Add("slot7_item", new DynamicMemoryValue("slot7_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_7"].Address));
            Inventory.Add("slot8_item", new DynamicMemoryValue("slot8_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_8"].Address));
            Inventory.Add("slot9_item", new DynamicMemoryValue("slot9_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_9"].Address));
            Inventory.Add("slot10_item", new DynamicMemoryValue("slot10_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_10"].Address));
            Inventory.Add("slot11_item", new DynamicMemoryValue("slot11_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_11"].Address));
            Inventory.Add("slot12_item", new DynamicMemoryValue("slot12_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_12"].Address));
            Inventory.Add("slot13_item", new DynamicMemoryValue("slot13_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_13"].Address));
            Inventory.Add("slot14_item", new DynamicMemoryValue("slot14_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_14"].Address));
            Inventory.Add("slot15_item", new DynamicMemoryValue("slot15_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_15"].Address));
            Inventory.Add("slot16_item", new DynamicMemoryValue("slot16_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_16"].Address));

            // Inventory items durability
            Inventory.Add("slot1_durability", new DynamicMemoryValue("slot1_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_1"].Address));
            Inventory.Add("slot2_durability", new DynamicMemoryValue("slot2_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_2"].Address));
            Inventory.Add("slot3_durability", new DynamicMemoryValue("slot3_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_3"].Address));
            Inventory.Add("slot4_durability", new DynamicMemoryValue("slot4_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_4"].Address));
            Inventory.Add("slot5_durability", new DynamicMemoryValue("slot5_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_5"].Address));
            Inventory.Add("slot6_durability", new DynamicMemoryValue("slot6_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_6"].Address));
            Inventory.Add("slot7_durability", new DynamicMemoryValue("slot7_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_7"].Address));
            Inventory.Add("slot8_durability", new DynamicMemoryValue("slot8_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_8"].Address));
            Inventory.Add("slot9_durability", new DynamicMemoryValue("slot9_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_9"].Address));
            Inventory.Add("slot10_durability", new DynamicMemoryValue("slot10_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_10"].Address));
            Inventory.Add("slot11_durability", new DynamicMemoryValue("slot11_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_11"].Address));
            Inventory.Add("slot12_durability", new DynamicMemoryValue("slot12_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_12"].Address));
            Inventory.Add("slot13_durability", new DynamicMemoryValue("slot13_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_13"].Address));
            Inventory.Add("slot14_durability", new DynamicMemoryValue("slot14_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_14"].Address));
            Inventory.Add("slot15_durability", new DynamicMemoryValue("slot15_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_15"].Address));
            Inventory.Add("slot16_durability", new DynamicMemoryValue("slot16_durability", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["item_slot_durability_16"].Address));

            // Key items inventory slots
            Inventory.Add("slot1_key_item", new DynamicMemoryValue("slot1_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_1"].Address));
            Inventory.Add("slot2_key_item", new DynamicMemoryValue("slot2_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_2"].Address));
            Inventory.Add("slot3_key_item", new DynamicMemoryValue("slot3_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_3"].Address));
            Inventory.Add("slot4_key_item", new DynamicMemoryValue("slot4_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_4"].Address));
            Inventory.Add("slot5_key_item", new DynamicMemoryValue("slot5_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_5"].Address));
            Inventory.Add("slot6_key_item", new DynamicMemoryValue("slot6_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_6"].Address));
            Inventory.Add("slot7_key_item", new DynamicMemoryValue("slot7_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_7"].Address));
            Inventory.Add("slot8_key_item", new DynamicMemoryValue("slot8_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_8"].Address));
            Inventory.Add("slot9_key_item", new DynamicMemoryValue("slot9_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_9"].Address));
            Inventory.Add("slot10_key_item", new DynamicMemoryValue("slot10_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_10"].Address));
            Inventory.Add("slot11_key_item", new DynamicMemoryValue("slot11_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_11"].Address));
            Inventory.Add("slot12_key_item", new DynamicMemoryValue("slot12_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_12"].Address));
            Inventory.Add("slot13_key_item", new DynamicMemoryValue("slot13_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_13"].Address));
            Inventory.Add("slot14_key_item", new DynamicMemoryValue("slot14_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_14"].Address));
            Inventory.Add("slot15_key_item", new DynamicMemoryValue("slot15_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_15"].Address));
            Inventory.Add("slot16_key_item", new DynamicMemoryValue("slot16_key_item", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["key_item_slot_16"].Address));

            // Weapon inventory slots
            Inventory.Add("slot1_weapon", new DynamicMemoryValue("slot1_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_1"].Address));
            Inventory.Add("slot2_weapon", new DynamicMemoryValue("slot2_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_2"].Address));
            Inventory.Add("slot3_weapon", new DynamicMemoryValue("slot3_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_3"].Address));
            Inventory.Add("slot4_weapon", new DynamicMemoryValue("slot4_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_4"].Address));
            Inventory.Add("slot5_weapon", new DynamicMemoryValue("slot5_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_5"].Address));
            Inventory.Add("slot6_weapon", new DynamicMemoryValue("slot6_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_6"].Address));
            Inventory.Add("slot7_weapon", new DynamicMemoryValue("slot7_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_7"].Address));
            Inventory.Add("slot8_weapon", new DynamicMemoryValue("slot8_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_8"].Address));
            Inventory.Add("slot9_weapon", new DynamicMemoryValue("slot9_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_9"].Address));
            Inventory.Add("slot10_weapon", new DynamicMemoryValue("slot10_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_10"].Address));
            Inventory.Add("slot11_weapon", new DynamicMemoryValue("slot11_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_11"].Address));
            Inventory.Add("slot12_weapon", new DynamicMemoryValue("slot12_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_12"].Address));
            Inventory.Add("slot13_weapon", new DynamicMemoryValue("slot13_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_13"].Address));
            Inventory.Add("slot14_weapon", new DynamicMemoryValue("slot14_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_14"].Address));
            Inventory.Add("slot15_weapon", new DynamicMemoryValue("slot15_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_15"].Address));
            Inventory.Add("slot16_weapon", new DynamicMemoryValue("slot16_weapon", _zoktaiAddresses.Misc["stat"].Address, _zoktaiAddresses.Inventory["weapon_slot_16"].Address));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _shinbokAddresses.Misc["room"].Address, _shinbokAddresses.Django["hp"].Address));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_vit"].Address));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_spr"].Address));
            Django.Add("base_str", new DynamicMemoryValue("base_str", _shinbokAddresses.Misc["stat"].Address, _shinbokAddresses.Django["base_str"].Address));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
