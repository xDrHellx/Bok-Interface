using System.Collections.Generic;

namespace BokInterface.Addresses {
    /// <summary>Main class for Boktai: The Sun is in Your Hand memory addresses</summary>
    public class BoktaiAddresses {

        /// <summary>Django-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Django = new Dictionary<string, MemoryAddress>();

        /// <summary>Inventory-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Inventory = new Dictionary<string, MemoryAddress>();

        /// <summary>Garding-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Gardening = new Dictionary<string, MemoryAddress>();

        /// <summary>Map & dungeon-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Map = new Dictionary<string, MemoryAddress>();

        /// <summary>Misc memory addresses</summary>
        public IDictionary<string, MemoryAddress> Misc = new Dictionary<string, MemoryAddress>();

        /// <summary>Coffin-related memory addresses</summary>
        public IDictionary<string, MemoryAddress> Coffin = new Dictionary<string, MemoryAddress>();

        public BoktaiAddresses() {
            InitDjangoAddresses();
            InitInventoryAddresses();
            InitMapAddresses();
            InitCoffinAddresses();
            InitMiscAddresses();
        }

        private void InitDjangoAddresses() {
            Django.Add("x_position", new MemoryAddress(0x0203D8F0, note: "Django X position", domain: "EWRAM"));
            Django.Add("y_position", new MemoryAddress(0x0203D8F4, note: "Django Y position", domain: "EWRAM"));
            Django.Add("z_position", new MemoryAddress(0x0203D8F2, note: "Django Z position", domain: "EWRAM"));
            Django.Add("current_lens_level", new MemoryAddress(0x18, type: "U32", domain: "IWRAM"));
            Django.Add("current_position", new MemoryAddress(0x24, note: "Current position", domain: "IWRAM"));
            // Django.Add("max_hp_minus_current_hp", new MemoryAddress(0x11a, note: "Max HP - Current HP", domain: "IWRAM"));
            Django.Add("enduranut_buffered_damage", new MemoryAddress(0x79A, domain: "IWRAM"));
            Django.Add("astro_battery_unlock_password", new MemoryAddress(0x7DC, type: "U32", domain: "IWRAM"));
            Django.Add("dark_loans", new MemoryAddress(0x0203D878, type: "U32", domain: "EWRAM"));
            Django.Add("solar_station", new MemoryAddress(0x0203DCE8, domain: "EWRAM"));
            Django.Add("max_hp", new MemoryAddress(0x0203D840, domain: "EWRAM"));
            Django.Add("current_hp", new MemoryAddress(0x0203D8FA, domain: "EWRAM"));
        }

        private void InitInventoryAddresses() {
            Inventory.Add("grenade_amounts", new MemoryAddress(0x0203D80A, domain: "EWRAM"));
            Inventory.Add("battery_charges", new MemoryAddress(0x0203D818, domain: "EWRAM"));
            Inventory.Add("equipped_lens", new MemoryAddress(0x0203D88C, domain: "EWRAM"));
            Inventory.Add("equipped_frame", new MemoryAddress(0x0203D88E, domain: "EWRAM"));
            Inventory.Add("equipped_grenades", new MemoryAddress(0x0203D890, domain: "EWRAM"));
            Inventory.Add("equipped_battery", new MemoryAddress(0x0203D892, domain: "EWRAM"));
            Inventory.Add("pineapple_grenade_charge", new MemoryAddress(0x0203D894, domain: "EWRAM"));
            // Inventory.Add("astro_battery_unlocked", new MemoryAddress(0x0203d8b0, type: "U32", domain: "EWRAM")); // must be set to *(*0x03001C90 + 0x7DC)
            Inventory.Add("item_size", new MemoryAddress(0x0203DC18, domain: "EWRAM"));
            Inventory.Add("item_ids", new MemoryAddress(0x0203DC1A, domain: "EWRAM"));
            Inventory.Add("item_amounts", new MemoryAddress(0x0203DC6E, domain: "EWRAM"));
            Inventory.Add("grenades_limit", new MemoryAddress(0x0203DCC2, domain: "EWRAM"));
            Inventory.Add("lenses", new MemoryAddress(0x0203DCC4, note: "bitmask, 1 bit per lens & level", type: "U32", domain: "EWRAM"));
            Inventory.Add("frames", new MemoryAddress(0x0203DCC8, note: "bitmask", type: "U32", domain: "EWRAM"));
            Inventory.Add("batteries", new MemoryAddress(0x0203DCD0, note: "bitmask", type: "U32", domain: "EWRAM"));
            Inventory.Add("lenses_exp", new MemoryAddress(0x0203DCD8, domain: "EWRAM"));
        }

        private void InitMapAddresses() {
            Map.Add("current_map_id", new MemoryAddress(0x0203D900, domain: "EWRAM"));
            Map.Add("dungeon_igt", new MemoryAddress(0x0203D910, type: "U32", domain: "EWRAM"));
            Map.Add("dungeon_charged_energy", new MemoryAddress(0x0203D914, type: "U32", domain: "EWRAM"));
            Map.Add("dungeon_amount_detected", new MemoryAddress(0x0203D918, note: "Number of times seen (current dungeon)", domain: "EWRAM"));
            Map.Add("dungeons_completion_flags", new MemoryAddress(0x0203D928, type: "U32", domain: "EWRAM"));
            Map.Add("dungeons_related_flags", new MemoryAddress(0x0203DAF0, domain: "EWRAM"));
            Map.Add("current_area_id", new MemoryAddress(0x0203DADC, type: "U32", domain: "EWRAM"));
            Map.Add("dungeon_base_score", new MemoryAddress(0x0203F0CC, domain: "EWRAM"));
            Map.Add("map_data", new MemoryAddress(0x03004610, type: "U32", domain: "IWRAM"));
            Map.Add("permafrost_ceiling", new MemoryAddress(0x02007542, domain: "EWRAM"));
            Map.Add("piledriver_top_left", new MemoryAddress(0x02008436, domain: "EWRAM"));
            Map.Add("piledriver_top_right", new MemoryAddress(0x02008956, domain: "EWRAM"));
            Map.Add("piledriver_bottom_left", new MemoryAddress(0x02008BE6, domain: "EWRAM"));
            Map.Add("piledriver_bottom_right", new MemoryAddress(0x020086C6, domain: "EWRAM"));
        }

        private void InitCoffinAddresses() {
            Coffin.Add("actor", new MemoryAddress(0x03001C20, type: "U32", domain: "IWRAM"));
            Coffin.Add("damage", new MemoryAddress(0x248, note: "Stun/Damage dealt to the coffin (coffin becomes stunned when the value reaches 1200 and then stays stunned for that duration, resets to 0 when coffin goes into shaking phase)", domain: "IWRAM"));
            Coffin.Add("windup_timer", new MemoryAddress(0x272, note: "Frames until the coffin goes into the windup phase (increases only while Django holds the chain)", domain: "IWRAM"));
            Coffin.Add("shake_timer", new MemoryAddress(0x278, note: "Time before the coffin is able to damage Django and becomes immovable (ranges from 0-127 and resets to 0 when the coffin goes into the shake phase)", domain: "IWRAM"));
            Coffin.Add("shake_duration", new MemoryAddress(0x288, note: "Duration of the shake (seems to be randomly chosen when entering rooms and after shaking)", domain: "IWRAM"));
            Coffin.Add("own_movement_timer", new MemoryAddress(0x29C, note: "Frames until the coffin starts moving on its own", domain: "IWRAM"));
            Coffin.Add("x_position", new MemoryAddress(0x44, domain: "IWRAM"));
            Coffin.Add("y_position", new MemoryAddress(0x48, domain: "IWRAM"));
        }

        private void InitMiscAddresses() {
            Misc.Add("boss_hp", new MemoryAddress(0x02001B5E, note: "Read-only", domain: "EWRAM"));
            Misc.Add("trap_hp", new MemoryAddress(0x02001B6A, note: "Read-only", domain: "EWRAM"));
            Misc.Add("link_points", new MemoryAddress(0x02005610, note: "Link points in status room", domain: "EWRAM"));
            Misc.Add("link_points_source", new MemoryAddress(0x0203DD2C, domain: "EWRAM"));
            Misc.Add("link_battles_total", new MemoryAddress(0x0203DD2E, domain: "EWRAM"));
            Misc.Add("link_battles_won", new MemoryAddress(0x0203DD30, domain: "EWRAM"));
            Misc.Add("items_exchanges_amount", new MemoryAddress(0x0203DD32, domain: "EWRAM"));
            Misc.Add("rng", new MemoryAddress(0x0203C800, note: "RNG table (generated by the bootstrap RNG once on power-on)", type: "U32", domain: "EWRAM"));
            Misc.Add("stat", new MemoryAddress(0x0203D800, note: "For stats & inventory", type: "U32", domain: "EWRAM"));
            Misc.Add("stat_backup", new MemoryAddress(0x0203E000, note: "Backup of stats & inventory (created on screen transitions, reloaded on death, saved to save file when saving)", type: "U32", domain: "EWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x0203E800, note: "Story progress & dungeon states", type: "U32", domain: "EWRAM"));
            Misc.Add("world_state_backup", new MemoryAddress(0x0203EC00, note: "Backup of story progress & dungeon states", type: "U32", domain: "EWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x0203F000, type: "U32", domain: "EWRAM"));
            Misc.Add("playthrough_count", new MemoryAddress(0x0203D8B6, note: "0 = New Game, 1 = NG+, 2 = NG++, ...", domain: "EWRAM"));
            Misc.Add("frames_since_new_file", new MemoryAddress(0x0203D8FC, note: "IGT frames since creating save file", type: "U32", domain: "EWRAM"));
            Misc.Add("purification_immortal_hp", new MemoryAddress(0x0203DAF4, type: "U32", domain: "EWRAM"));
            // Misc.Add("story_progress", new MemoryAddress(0x0203E80E, domain: "EWRAM"));
            Misc.Add("item_effect_active", new MemoryAddress(0x03001C40, note: "bitmask", domain: "IWRAM"));
            Misc.Add("item_effect_duration", new MemoryAddress(0x03001C60, domain: "IWRAM"));
            Misc.Add("grenade_effect_active", new MemoryAddress(0x03001C58, note: "bitmask", domain: "IWRAM"));
            Misc.Add("grenade_effect_duration", new MemoryAddress(0x03001C54, domain: "IWRAM"));
            Misc.Add("bootstrap_rng_seed", new MemoryAddress(0x03004478, domain: "IWRAM"));
            Misc.Add("actor", new MemoryAddress(0x03001C90, note: "Pointer to Django's actor data", type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x030046E8, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x030046EA, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x030046EC, note: "Camera Z position", domain: "IWRAM"));
            Misc.Add("action_difficulty", new MemoryAddress(0x0203E8BC, note: "Action difficulty setting (0 = Easy | 1 = Normal | 2 = Hard)", domain: "System Bus"));
            Misc.Add("rng_index", new MemoryAddress(0x03004620, type: "U32", domain: "IWRAM"));
        }
    }
}
