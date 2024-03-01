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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["current_hp"]));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", zoktaiAddresses.Misc["stat"], zoktaiAddresses.Django["current_ene"]));

            // U16
            U16.Add("vit", new U16MemoryValue("vit", zoktaiAddresses.Django["persistent_vit"]));
            U16.Add("spr", new U16MemoryValue("spr", zoktaiAddresses.Django["persistent_spr"]));
            U16.Add("str", new U16MemoryValue("str", zoktaiAddresses.Django["persistent_str"]));
            U16.Add("agi", new U16MemoryValue("agi", zoktaiAddresses.Django["persistent_agi"]));
            
            U16.Add("sword_skill", new U16MemoryValue("sword_skill", zoktaiAddresses.Django["sword_skill_exp"]));
            U16.Add("spear_skill", new U16MemoryValue("spear_skill", zoktaiAddresses.Django["spear_skill_exp"]));
            U16.Add("hammer_skill", new U16MemoryValue("hammer_skill", zoktaiAddresses.Django["hammer_skill_exp"]));
            U16.Add("fists_skill", new U16MemoryValue("fists_skill", zoktaiAddresses.Django["fists_skill_exp"]));
            U16.Add("gun_skill", new U16MemoryValue("gun_skill", zoktaiAddresses.Django["gun_skill_exp"]));
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
