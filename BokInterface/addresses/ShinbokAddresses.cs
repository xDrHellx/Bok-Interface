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
            Django.Add("current_hp", new MemoryAddress(0x424, domain: "EWRAM"));
            Django.Add("current_ene", new MemoryAddress(0x428, domain: "EWRAM"));
            Django.Add("current_trc", new MemoryAddress(0x42C, domain: "EWRAM"));

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

            // 0x18 + 2 * stat_id
            _note = "Stat points put into ";
            Django.Add("base_vit", new MemoryAddress(0x18, note: _note + "VIT", domain: "EWRAM"));
            Django.Add("base_spr", new MemoryAddress(0x1A, note: _note + "SPR", domain: "EWRAM"));
            Django.Add("base_str", new MemoryAddress(0x1C, note: _note + "STR", domain: "EWRAM"));
            Django.Add("stat_points_to_allocate", new MemoryAddress(0x442, domain: "EWRAM"));

            // Django.Add("equips_vit", 0x18);
            // Django.Add("equips_spr", 0x32C);
            // Django.Add("equips_str", 0x1C);

            /*
            equips vit
            00B55
            
            equips spr

            0200b918 => 0813EDFA => 0813F062

            00B558
            00C8B8

            equips str
            00B55A
            */

            // Add Misc addresses
            // Misc.Add("equips_stat", 0x02004094);
            // 02004094
            Misc.Add("room", new MemoryAddress(0x02000580, note: "Current room", type: "U32", domain: "EWRAM"));
            Misc.Add("stat", new MemoryAddress(0x02000710, note: "Stats & inventory", type: "U32", domain: "EWRAM"));
            Misc.Add("world_state", new MemoryAddress(0x0203DB08, note: "Story progress & dungeon states", type: "U32", domain: "EWRAM"));
            Misc.Add("scratch", new MemoryAddress(0x0203E308, type: "U32", domain: "EWRAM"));
            Misc.Add("map_data", new MemoryAddress(0x030052F4, type: "U32", domain: "IWRAM"));
            Misc.Add("x_camera", new MemoryAddress(0x03005418, note: "Camera X position", domain: "IWRAM"));
            Misc.Add("y_camera", new MemoryAddress(0x0300541A, note: "Camera Y position", domain: "IWRAM"));
            Misc.Add("z_camera", new MemoryAddress(0x0300541C, note: "Camera Z position", domain: "IWRAM"));
        }
    }
}