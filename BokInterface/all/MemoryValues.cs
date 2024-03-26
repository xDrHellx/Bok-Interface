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
        public IDictionary<string, U16MemoryValue> U16 = new Dictionary<string, U16MemoryValue>();

        /// <summary>U32 memory values</summary>
        public IDictionary<string, U32MemoryValue> U32 = new Dictionary<string, U32MemoryValue>();

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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_hp"]));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_ene"]));

            Django.Add("level", new DynamicMemoryValue("level", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["level"]));
            Django.Add("stat_points", new DynamicMemoryValue("stat_points", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["stat_points_to_allocate"]));

            // Stats applied in the current room
            Django.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_vit"]));
            Django.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_spr"]));
            Django.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_str"]));
            Django.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["current_stat"], zoktaiAddresses.Django["current_agi"]));

            // Skill
            Django.Add("sword_skill", new DynamicMemoryValue("sword_skill", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["sword_skill_exp"]));
            Django.Add("spear_skill", new DynamicMemoryValue("spear_skill", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["spear_skill_exp"]));
            Django.Add("hammer_skill", new DynamicMemoryValue("hammer_skill", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["hammer_skill_exp"]));
            Django.Add("fists_skill", new DynamicMemoryValue("fists_skill", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["fists_skill_exp"]));
            Django.Add("gun_skill", new DynamicMemoryValue("gun_skill", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["gun_skill_exp"]));

            // Stats that will be applied when switching room
            Misc.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["persistent_vit"]));
            Misc.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["persistent_spr"]));
            Misc.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["persistent_str"]));
            Misc.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["persistent_agi"]));

            // U32
            U32.Add("exp", new U32MemoryValue("exp", zoktaiAddresses.Django["exp"]));
            U32.Add("total_exp_until_next_level", new U32MemoryValue("total_exp_until_next_level", zoktaiAddresses.Django["total_exp_until_next_level"]));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", shinbokAddresses.Misc["room"], shinbokAddresses.Django["hp"]));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["base_vit"]));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["base_spr"]));
            Django.Add("base_str", new DynamicMemoryValue("base_str", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["base_str"]));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
