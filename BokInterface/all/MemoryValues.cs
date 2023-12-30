using System.Collections.Generic;
using BokInterface.All;
using BokInterface.Boktai;
using BokInterface.Zoktai;
using BokInterface.Shinbok;
using BokInterface.LunarKnights;

namespace BokInterface {

    /// <summary>Class containing instances of memory values for the current game</summary>
    class MemoryValues {

        private readonly BoktaiAddresses boktaiAddresses = new();
        private readonly ZoktaiAddresses zoktaiAddresses = new();
        private readonly ShinbokAddresses shinbokAddresses = new();
        private readonly LunarKnightsAddresses lunarKnightsAddresses = new();

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

            this.ClearLists();
            
            switch(shorterGameName) {
                case "Boktai":
                    this.InitializeBoktaiList();
                    break;
                case "Zoktai":
                    this.InitializeZoktaiList();
                    break;
                case "Shinbok":
                    this.InitializeShinbokList();
                    break;
                case "LunarKnights":
                    this.InitializeLunarKnightsList();
                    break;
                default:
                    break;
            }
        }

        private void ClearLists() {
            this.Django.Clear();
            this.Solls.Clear();
            this.Bike.Clear();
            this.Misc.Clear();
        }

        private void InitializeBoktaiList() {

        }

        private void InitializeZoktaiList() {
            
        }

        private void InitializeShinbokList() {

            this.Django.Add("currentHp", new DynamicMemoryValue("currentHp", shinbokAddresses.Misc["room"], shinbokAddresses.Django["hp"]));
            this.Django.Add("baseVit", new DynamicMemoryValue("baseVit", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseVit"]));
            this.Django.Add("baseSpr", new DynamicMemoryValue("baseSpr", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseSpr"]));
            this.Django.Add("baseStr", new DynamicMemoryValue("baseStr", shinbokAddresses.Misc["stat"], shinbokAddresses.Django["baseStr"]));
        }

        private void InitializeLunarKnightsList() {

        }
    }
}