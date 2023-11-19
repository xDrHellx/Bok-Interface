using System.Collections.Generic;

namespace BokInterface.Shinbok {

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
        /// <para>Short memory addresses</para>
        /// <para>
        ///     These are used for some cases where the values we need are stored in different memory addresses <br/>
        ///     For example the memory address for Django's current HP is different based on which "room sections" he is in
        /// </para>
        /// </summary>
        // public IDictionary<string, uint> Short = new Dictionary<string, uint>();

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
            // Django.Add("hp", 0x03C428);
            // Django.Add("ene", 0x03C42C);
            // Django.Add("trc", 0x03CA08);
            // Django.Add("stat_points_to_allocate", 0x03C442);
            // Django.Add("level", 0x03C440);
            // Django.Add("current_exp", 0x03C448);
            // Django.Add("exp_until_next_level", 0x001BC8);
            // Django.Add("vit", 0x03C418);
            // Django.Add("spr", 0x03C41A);
            // Django.Add("str", 0x03C41C);

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
            // Django.Add("ene", 0x428); // NEED TO FIND OTHER VALUE
            Django.Add("vit", 0x41C);
            Django.Add("spr", 0x41C);
            Django.Add("str", 0x41C); //0x48B6

            // Add Misc addresses
            Misc.Add("room", 0x02000580);
            Misc.Add("vit", 0x02004094);
            Misc.Add("spr", 0x030067E0);
            Misc.Add("str", 0x081753A5); //030033D0

            // Short.Add("??? for maxHp", 0x0200B5E4); // Not correct 00BA0A = 0200B5E4 + 00000426
            // Short.Add("django_maxHp", 0x426);
        }
    }
}