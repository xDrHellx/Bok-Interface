using System.Collections.Generic;
using BokInterface.All;
using BokInterface.Addresses;

namespace BokInterface {

    /// <summary>Class containing instances of memory values for the current game</summary>
    class MemoryValues {

        #region Properties

        private readonly ZoktaiAddresses zoktaiAddresses = new();
        private readonly ShinbokAddresses shinbokAddresses = new();

        /// <summary>Django-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Django = new Dictionary<string, DynamicMemoryValue>();

        /// <summary>Solls-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Solls = new Dictionary<string, DynamicMemoryValue>();

        /// <summary>Bike-related memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Bike = new Dictionary<string, DynamicMemoryValue>();

        /// <summary>Misc memory values</summary>
        public IDictionary<string, DynamicMemoryValue> Misc = new Dictionary<string, DynamicMemoryValue>();

        /// <summary>U16 memory values</summary>
        public IDictionary<string, MemoryValue> U16 = new Dictionary<string, MemoryValue>();

        /// <summary>U32 memory values</summary>
        public IDictionary<string, MemoryValue> U32 = new Dictionary<string, MemoryValue>();

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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_hp"].address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_ene"].address));

            Django.Add("level", new DynamicMemoryValue("level", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["level"].address));
            Django.Add("exp", new DynamicMemoryValue("exp", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["exp"].address, "U32"));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["stat_points_to_allocate"].address));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_vit"].address));
            Django.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_spr"].address));
            Django.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_str"].address));
            Django.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["current_stat"].address, zoktaiAddresses.Django["current_agi"].address));

            // Skill
            Django.Add("sword_skill", new DynamicMemoryValue("sword_skill", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["sword_skill_exp"].address));
            Django.Add("spear_skill", new DynamicMemoryValue("spear_skill", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["spear_skill_exp"].address));
            Django.Add("hammer_skill", new DynamicMemoryValue("hammer_skill", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["hammer_skill_exp"].address));
            Django.Add("fists_skill", new DynamicMemoryValue("fists_skill", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["fists_skill_exp"].address));
            Django.Add("gun_skill", new DynamicMemoryValue("gun_skill", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["gun_skill_exp"].address));

            // Stats that will be applied when switching room
            Misc.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["persistent_vit"].address));
            Misc.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["persistent_spr"].address));
            Misc.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["persistent_str"].address));
            Misc.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["stat"].address, zoktaiAddresses.Django["persistent_agi"].address));

            // U32
            U32.Add("total_exp_until_next_level", new MemoryValue("total_exp_until_next_level", zoktaiAddresses.Django["total_exp_until_next_level"].address, zoktaiAddresses.Django["total_exp_until_next_level"].type, zoktaiAddresses.Django["total_exp_until_next_level"].domain));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", shinbokAddresses.Misc["room"].address, shinbokAddresses.Django["hp"].address));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", shinbokAddresses.Misc["stat"].address, shinbokAddresses.Django["base_vit"].address));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", shinbokAddresses.Misc["stat"].address, shinbokAddresses.Django["base_spr"].address));
            Django.Add("base_str", new DynamicMemoryValue("base_str", shinbokAddresses.Misc["stat"].address, shinbokAddresses.Django["base_str"].address));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
