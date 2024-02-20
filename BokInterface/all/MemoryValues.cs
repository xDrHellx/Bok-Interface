using System.Collections.Generic;
using BokInterface.All;
using BokInterface.Addresses;

namespace BokInterface {

    /// <summary>Class containing instances of memory values for the current game</summary>
    class MemoryValues {

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
            Django.Add("current_hp", new DynamicMemoryValue("current_hp", zoktaiAddresses.Django["persistent_hp"], zoktaiAddresses.Django["current_hp"]));
            Django.Add("current_ene", new DynamicMemoryValue("current_ene", zoktaiAddresses.Django["persistent_ene"], zoktaiAddresses.Django["current_ene"]));

            // Stats
            Django.Add("vit", new DynamicMemoryValue("vit", zoktaiAddresses.Django["vit"], zoktaiAddresses.Django["vit"]));
            Django.Add("spr", new DynamicMemoryValue("spr", zoktaiAddresses.Django["spr"], zoktaiAddresses.Django["spr"]));
            Django.Add("str", new DynamicMemoryValue("str", zoktaiAddresses.Django["str"], zoktaiAddresses.Django["str"]));
            Django.Add("agi", new DynamicMemoryValue("agi", zoktaiAddresses.Django["agi"], zoktaiAddresses.Django["agi"]));
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
