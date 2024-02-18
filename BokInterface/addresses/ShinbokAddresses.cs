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
        public IDictionary<string, uint> Django = new Dictionary<string, uint>();
        
        /// <summary>Solls-related memory addresses</summary>
        public IDictionary<string, uint> Solls = new Dictionary<string, uint>();
        
        /// <summary>
        /// <para>Bike-related memory addresses</para>
        /// <para>
        ///     About the bars and scrolling : <br/>
        ///     - If HP and ENE are modified during races, the bars will update automatically <br/>
        ///     - Freezing the scrolling value will NOT stop Django from moving
        /// </para>
        /// </summary>
        public IDictionary<string, uint> Bike = new Dictionary<string, uint>();

        /// <summary>
        /// <para>Misc memory addresses</para>
        /// <para>
        ///     These are used in combination with other memory addresses to get / set values that are "dynamic" <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        public IDictionary<string, uint> Misc = new Dictionary<string, uint>();

        public ShinbokAddresses() {

            // Add Django addresses
            // Django.Add("ene", 0x03C42C);
            // Django.Add("trc", 0x03CA08);
            // Django.Add("stat_points_to_allocate", 0x03C442);
            // Django.Add("level", 0x03C440);
            // Django.Add("current_exp", 0x03C448);
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

            // Add Django addresses
            Django.Add("hp", 0x424);

            Django.Add("x_position", 0x0203C430);
            Django.Add("y_position", 0x0203C434);
            Django.Add("z_position", 0x0203C432);

            // 0x18 + 2 * stat_id
            Django.Add("base_vit", 0x18);
            Django.Add("base_spr", 0x1A);
            Django.Add("base_str", 0x1C);

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
            Misc.Add("room", 0x02000580);
            Misc.Add("stat", 0x02000710);
            Misc.Add("map_data", 0x030052F4);
            Misc.Add("x_camera", 0x03005418);
            Misc.Add("y_camera", 0x0300541A);
            Misc.Add("z_camera", 0x0300541C);
        }
    }
}