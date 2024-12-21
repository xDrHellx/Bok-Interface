using System.Collections.Generic;

namespace BokInterface.Addresses {

    /// <summary>Main class for Boktai 3 / Shinbok memory addresses</summary>
    public class ShinbokAddresses {

        /// <summary>
        /// <para>Django-related memory addresses</para>
        /// <para>
        ///     About level and EXP : <br/>
        ///     - If the EXP is too high, level will keep increasing automatically <br/>
        ///     - EXP Until next level adjusts itself automatically
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Django = new Dictionary<string, MemoryAddress>();

        /// <summary>Solls-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Solls = new Dictionary<string, MemoryAddress>();

        /// <summary>Inventory-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Bike-related memory addresses</para>
        /// <para>
        ///     About the bars and scrolling : <br/>
        ///     - If HP and ENE are modified during races, the bars will update automatically <br/>
        ///     - Freezing the scrolling value will NOT stop Django from moving
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Bike = new Dictionary<string, MemoryAddress>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        /// <summary>Note for MemoryAddress instances (for less repetition)</summary>
        private string _note = "";

        public ShinbokAddresses() {

            // Add Django addresses
            Django.Add("x_position", new MemoryAddress(0x30, note: "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x34, note: "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x32, note: "Django Z position", domain: "EWRAM"));

            // Current stats
            _note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Django.Add("current_hp", new MemoryAddress(0x424, domain: "EWRAM"));
            Django.Add("current_ene", new MemoryAddress(0x428, domain: "EWRAM"));
            Django.Add("current_trc", new MemoryAddress(0x42C, domain: "EWRAM"));

            // TODO : Base points are only applied after switching rooms : fix current_base_
            // TODO : Cards points are only applied after switching rooms : fix current_cards_
            // TODO : Equips points don't stay after switching rooms : add persistent_equips_

            // VIT
            Django.Add("current_base_vit", new MemoryAddress(0x18, note: _note, domain: "EWRAM")); // need to find proper address
            Django.Add("current_cards_vit", new MemoryAddress(0x20, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM")); // need to find proper address
            Django.Add("current_equips_vit", new MemoryAddress(0x332, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_vit", new MemoryAddress(0x41C, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));
            // Django.Add("current_total_vit", new MemoryAddress(0xFCB636, note: "Total stat points (Base + Cards + Accessories)", domain: "EWRAM"));

            // SPR
            Django.Add("current_base_spr", new MemoryAddress(0x1A, note: _note, domain: "EWRAM")); // need to find proper address
            Django.Add("current_cards_spr", new MemoryAddress(0x22, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM")); // need to find proper address
            Django.Add("current_equips_spr", new MemoryAddress(0x334, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_spr", new MemoryAddress(0x41E, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));
            // Django.Add("current_total_spr", new MemoryAddress(0xFCB638, note: "Total stat points (Base + Cards + Accessories)", domain: "EWRAM"));

            // STR
            Django.Add("current_base_str", new MemoryAddress(0x1C, note: _note, domain: "EWRAM")); // need to find proper address
            Django.Add("current_cards_str", new MemoryAddress(0x24, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM")); // need to find proper address
            Django.Add("current_equips_str", new MemoryAddress(0x336, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_str", new MemoryAddress(0x420, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));
            // Django.Add("current_total_str", new MemoryAddress(0xFCB640, note: "Total stat points (Base + Cards + Accessories)", domain: "EWRAM"));

            // Persistent stats (used on screen transitions & save data)
            _note = "Also corresponds to values from Save Data";
            Django.Add("persistent_base_vit", new MemoryAddress(0x18, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_vit", new MemoryAddress(0x20, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            Django.Add("persistent_base_spr", new MemoryAddress(0x1A, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_spr", new MemoryAddress(0x22, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            Django.Add("persistent_base_str", new MemoryAddress(0x1C, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_str", new MemoryAddress(0x24, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            Django.Add("stat_points_to_allocate", new MemoryAddress(0x442, domain: "EWRAM"));

            // Add Solls addresses
            // Solls.Add("solls_on_self", 0x03CBB0);
            // Solls.Add("solar_bank", 0x03CB7C);
            // Solls.Add("dark_loan", 0x03C90C);

            // EXP & level
            Django.Add("level", new MemoryAddress(0x440, domain: "EWRAM"));
            Django.Add("current_exp", new MemoryAddress(0x448, domain: "EWRAM"));
            // Django.Add("exp_until_next_level", 0x001BC8);

            // Add Solls addresses
            // Solls.Add("solls_on_self", 0x03CBB0);
            // Solls.Add("solar_bank", 0x03CB7C);
            // Solls.Add("dark_loan", 0x03C90C);

            // Add Bike addresses
            // Bike.Add("points", 0x03CBB2);
            // Bike.Add("hp", 0x00F6F8);
            // Bike.Add("ene", 0x00F6F4);
            // Bike.Add("base_speed", 0x00F730);
            // Bike.Add("scrolling", 0x0004E0);
            // Bike.Add("progress", 0x00F6AC);

            InitInventoryAddresses();

            // Add Misc addresses
            // Misc.Add("equips_stat", 0x02004094);
            Misc.Add("actor", new MemoryAddress(0x02000580, note: "Pointer to Django's actor data", type: "U32", domain: "EWRAM"));
            Misc.Add("stat", new MemoryAddress(0x02000710, note: "Stats & inventory", type: "U32", domain: "EWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x0203DB08, note: "Story progress & dungeon states", type: "U32", domain: "EWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x0203E308, type: "U32", domain: "EWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030052F4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x03005418, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x0300541A, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x0300541C, note: "Camera Z position", domain: "IWRAM"));
        }

        protected void InitInventoryAddresses() {

            /**
             * Inventory-related memory addresses
             * We set these using a loop to simplify
             */
            for (int i = 0; i < 16; i++) {

                int slotNumber = 1 + i;

                // Items & durability (2 bytes)
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("item_slot_" + slotNumber, new MemoryAddress(0xA0 + addressOffset, note: "Item slot", domain: "EWRAM"));
                Inventory.Add("item_slot_durability_" + slotNumber, new MemoryAddress(0x100 + addressOffset, note: "Item durability (for spoiling)", domain: "EWRAM"));

                // Key items (2 bytes)
                Inventory.Add("key_item_slot_" + slotNumber, new MemoryAddress(0x838 + addressOffset, note: "Key item inventory slot", domain: "EWRAM"));

                // Accessory slots (2 bytes)
                Inventory.Add("accessory_slot_" + slotNumber, new MemoryAddress(0x160 + addressOffset, note: "Accessory inventory slot", domain: "EWRAM"));

                // Weapon inventory slots & durability
                addressOffset = 0x10 * (uint)i;
                Inventory.Add("weapon_slot_" + slotNumber, new MemoryAddress(0x1C0 + addressOffset, note: "Weapon inventory slot", domain: "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_durability", new MemoryAddress(0x1C2 + addressOffset, note: "Weapon durability", domain: "EWRAM"));

                // SP effects
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_1", new MemoryAddress(0x1C4 + addressOffset, note: "1st SP ability", domain: "EWRAM"));
                Inventory.Add("weapon_slot_" + slotNumber + "_sp_ability_2", new MemoryAddress(0x1C8 + addressOffset, note: "2nd SP ability", domain: "EWRAM"));

                // Refine & weapon attack pattern
                Inventory.Add("weapon_slot_" + slotNumber + "_refine", new MemoryAddress(0x1CC + addressOffset, note: "Refine", domain: "EWRAM", type: "U8")); // 1 = II, 2 = III, others = normal weapon
                Inventory.Add("weapon_slot_" + slotNumber + "_pattern", new MemoryAddress(0x1CD + addressOffset, note: "Attack pattern ID", domain: "EWRAM", type: "U8"));
            }
        }
    }
}