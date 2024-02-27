using System.Collections.Generic;
using BokInterface.All;
using BokInterface.Addresses;

namespace BokInterface {

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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_hp"]));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_ene"]));

            // Stats
            Django.Add("vit", new DynamicMemoryValue("vit", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_vit"]));
            Django.Add("spr", new DynamicMemoryValue("spr", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_spr"]));
            Django.Add("str", new DynamicMemoryValue("str", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_str"]));
            Django.Add("agi", new DynamicMemoryValue("agi", _zoktaiAddresses.Misc["stat"], _zoktaiAddresses.Django["current_agi"]));

            // U16
            U16.Add("sword_skill", new U16MemoryValue("sword_skill", _zoktaiAddresses.Django["sword_skill_exp"]));
            U16.Add("spear_skill", new U16MemoryValue("spear_skill", _zoktaiAddresses.Django["spear_skill_exp"]));
            U16.Add("hammer_skill", new U16MemoryValue("hammer_skill", _zoktaiAddresses.Django["hammer_skill_exp"]));
            U16.Add("fists_skill", new U16MemoryValue("fists_skill", _zoktaiAddresses.Django["fists_skill_exp"]));
            U16.Add("gun_skill", new U16MemoryValue("gun_skill", _zoktaiAddresses.Django["gun_skill_exp"]));
        }

        private void InitializeShinbokList() {
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", _shinbokAddresses.Misc["room"], _shinbokAddresses.Django["hp"]));

            // Stats
            Django.Add("base_vit", new DynamicMemoryValue("base_vit", _shinbokAddresses.Misc["stat"], _shinbokAddresses.Django["base_vit"]));
            Django.Add("base_spr", new DynamicMemoryValue("base_spr", _shinbokAddresses.Misc["stat"], _shinbokAddresses.Django["base_spr"]));
            Django.Add("base_str", new DynamicMemoryValue("base_str", _shinbokAddresses.Misc["stat"], _shinbokAddresses.Django["base_str"]));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}
