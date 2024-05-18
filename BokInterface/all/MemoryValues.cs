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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_hp"].Address));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", _zoktaiAddresses.Misc["current_stat"].Address, _zoktaiAddresses.Django["current_ene"].Address));

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
