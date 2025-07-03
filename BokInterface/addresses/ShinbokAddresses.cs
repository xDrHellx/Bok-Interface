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
        private readonly string _note = "";

        public ShinbokAddresses() {
            InitDjangoAddresses();
            InitBikeAddresses();
            InitInventoryAddresses();
            InitGunAddresses();
            InitMiscAddresses();
        }

        private void InitDjangoAddresses() {

            // Position coordinates
            Django.Add("x_position", new MemoryAddress(0x30, note: "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x34, note: "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x32, note: "Django Z position", domain: "EWRAM"));

            // Current stats
            _note = "Used for damage calculations, will be copied to its Persistent equivalent on screen transition. Must be combined with the \"stat\" memory address' value";
            Django.Add("current_hp", new MemoryAddress(0x424, domain: "EWRAM"));
            Django.Add("current_ene", new MemoryAddress(0x428, domain: "EWRAM"));
            Django.Add("current_trc", new MemoryAddress(0x42C, domain: "EWRAM"));
            Django.Add("level", new MemoryAddress(0x40, domain: "EWRAM"));
            Django.Add("exp", new MemoryAddress(0x48, type: "U32", domain: "EWRAM"));
            Django.Add("total_exp_until_next_level", new MemoryAddress(0x1BC8, type: "U32", domain: "EWRAM"));
            Django.Add("stat_points", new MemoryAddress(0x42, domain: "EWRAM"));

            // VIT
            Django.Add("current_equips_vit", new MemoryAddress(0x332, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_vit", new MemoryAddress(0x41C, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));

            // SPR
            Django.Add("current_equips_spr", new MemoryAddress(0x334, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_spr", new MemoryAddress(0x41E, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));

            // STR
            Django.Add("current_equips_str", new MemoryAddress(0x336, note: "Stat points from accessories, " + _note.ToLower(), domain: "EWRAM"));
            Django.Add("current_sum_base_cards_str", new MemoryAddress(0x420, note: "Sum of base stat & points from cards, " + _note.ToLower(), domain: "EWRAM"));

            // Persistent stats (used on screen transitions & save data)
            _note = "Also corresponds to values from Save Data";
            Django.Add("persistent_hp", new MemoryAddress(0x28, domain: "EWRAM"));
            Django.Add("persistent_ene", new MemoryAddress(0x2C, domain: "EWRAM"));
            Django.Add("persistent_trc", new MemoryAddress(0x608, domain: "EWRAM"));

            Django.Add("persistent_base_vit", new MemoryAddress(0x18, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_vit", new MemoryAddress(0x20, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            Django.Add("persistent_base_spr", new MemoryAddress(0x1A, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_spr", new MemoryAddress(0x22, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            Django.Add("persistent_base_str", new MemoryAddress(0x1C, note: _note, domain: "EWRAM"));
            Django.Add("persistent_cards_str", new MemoryAddress(0x24, note: "Stat points from cards, " + _note.ToLower(), domain: "EWRAM"));

            // Add Solls addresses
            Solls.Add("solar_station", new MemoryAddress(0x77C, note: "Solar station balance", domain: "EWRAM"));
            Solls.Add("solar_bank", new MemoryAddress(0x7B0, note: "Solar bank balance", domain: "EWRAM"));
            Solls.Add("dark_loans", new MemoryAddress(0x50C, note: "Dark loans", domain: "EWRAM"));
        }

        private void InitBikeAddresses() {
            _note = "Equipped bike part";

            // Bike.Add("name", new MemoryAddress(0x780, note: "Bike name", domain: "EWRAM"));
            Bike.Add("points", new MemoryAddress(0x7B2, note: "Points from races", domain: "EWRAM"));
            Bike.Add("battle_matches", new MemoryAddress(0x7B4, note: "Number of Bike Battles matches", domain: "EWRAM"));
            Bike.Add("battle_wins", new MemoryAddress(0x7B6, note: "Number of Bike Battles won", domain: "EWRAM"));

            // Bike parts
            _note = "Equipped bike part";
            Bike.Add("front", new MemoryAddress(0x7A0, note: _note, domain: "EWRAM"));
            Bike.Add("tires", new MemoryAddress(0x7A2, note: _note, domain: "EWRAM"));
            Bike.Add("body", new MemoryAddress(0x7A4, note: _note, domain: "EWRAM"));
            Bike.Add("special", new MemoryAddress(0x7A6, note: _note, domain: "EWRAM"));
            Bike.Add("color", new MemoryAddress(0x81A, note: _note, domain: "EWRAM"));
            Bike.Add("option_1", new MemoryAddress(0x7A8, note: _note, domain: "EWRAM"));
            Bike.Add("option_2", new MemoryAddress(0x7AA, note: _note, domain: "EWRAM"));
            Bike.Add("option_3", new MemoryAddress(0x7AC, note: _note, domain: "EWRAM"));
            Bike.Add("option_4", new MemoryAddress(0x7AE, note: _note, domain: "EWRAM"));

            // Selected parts in bike menus
            // _note = "Selected bike part in menus";
            /*
                009F28	w	u	0	EWRAM	menu front
                009F2A	w	h	0	EWRAM	menu body
                009F2C	w	h	0	EWRAM	menu tires
                009F2E	w	h	0	EWRAM	menu special
                009F32	w	u	0	EWRAM	menu color
            */
        }

        /// <summary>Init Inventory-related memory addresses</summary>
        private void InitInventoryAddresses() {

            // Set these using a loop to simplify
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

        /// <summary>Init Gun-related memory addresses</summary>
        private void InitGunAddresses() {
            // Lenses
            for (int i = 0; i < 8; i++) {
                int slotNumber = 1 + i;
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("gun_lens_slot_" + slotNumber, new MemoryAddress(0x828 + addressOffset, note: "Gun lens inventory slot", domain: "EWRAM"));
            }

            // Frames
            for (int i = 0; i < 12; i++) {
                int slotNumber = 1 + i;
                uint addressOffset = 0x2 * (uint)i;
                Inventory.Add("gun_frame_slot_" + slotNumber, new MemoryAddress(0x4C0 + addressOffset, note: "Gun frame inventory slot", domain: "EWRAM"));
            }
        }

        private void InitMiscAddresses() {
            // Misc.Add("equips_stat", 0x02004094);
            Misc.Add("actor", new MemoryAddress(0x02000580, note: "Pointer to Django's actor data", type: "U32", domain: "EWRAM"));
            Misc.Add("stat", new MemoryAddress(0x02000710, note: "Stats & inventory", type: "U32", domain: "EWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x0203DB08, note: "Story progress & dungeon states", type: "U32", domain: "EWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x0203E308, type: "U32", domain: "EWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030052F4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x03005418, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x0300541A, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x0300541C, note: "Camera Z position", domain: "IWRAM"));
            Misc.Add("boss_hp", new MemoryAddress(0x0200EEC0, domain: "EWRAM"));
        }
    }
}