using System.Collections.Generic;

using BokInterface.Addresses;

namespace BokInterface.All {

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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_ene"].Address));

            Django.Add("level", new DynamicMemoryValue("level", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["level"].Address));
            Django.Add("exp", new DynamicMemoryValue("exp", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["exp"].Address, "U32"));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["stat_points_to_allocate"].Address));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_vit"].Address));
            Django.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_spr"].Address));
            Django.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_str"].Address));
            Django.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["current_stat"].Address, zoktaiAddresses.Django["current_agi"].Address));

            // Skill
            Django.Add("sword_skill", new DynamicMemoryValue("sword_skill", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["sword_skill_exp"].Address));
            Django.Add("spear_skill", new DynamicMemoryValue("spear_skill", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["spear_skill_exp"].Address));
            Django.Add("hammer_skill", new DynamicMemoryValue("hammer_skill", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["hammer_skill_exp"].Address));
            Django.Add("fists_skill", new DynamicMemoryValue("fists_skill", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["fists_skill_exp"].Address));
            Django.Add("gun_skill", new DynamicMemoryValue("gun_skill", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["gun_skill_exp"].Address));

            // Stats that will be applied when switching room
            Misc.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["persistent_vit"].Address));
            Misc.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["persistent_spr"].Address));
            Misc.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["persistent_str"].Address));
            Misc.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["stat"].Address, zoktaiAddresses.Django["persistent_agi"].Address));

            // U32
            U32.Add("total_exp_until_next_level", new MemoryValue("total_exp_until_next_level", zoktaiAddresses.Django["total_exp_until_next_level"].Address, zoktaiAddresses.Django["total_exp_until_next_level"].Type, zoktaiAddresses.Django["total_exp_until_next_level"].Domain));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", shinbokAddresses.Misc["room"].Address, shinbokAddresses.Django["hp"].Address));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", shinbokAddresses.Misc["stat"].Address, shinbokAddresses.Django["base_vit"].Address));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", shinbokAddresses.Misc["stat"].Address, shinbokAddresses.Django["base_spr"].Address));
            Django.Add("base_str", new DynamicMemoryValue("base_str", shinbokAddresses.Misc["stat"].Address, shinbokAddresses.Django["base_str"].Address));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
