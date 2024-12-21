using System.Collections.Generic;

using BokInterface.Addresses;
using BokInterface.All;

namespace BokInterface.Entities {
    /// <summary>Class for Zoktai entities instances and lists</summary>
    class ZoktaiEntities {

        public Dictionary<string, Character> AllCharacters = [],
            PlayableCharacters = [],
            Npcs = [];
        public Dictionary<string, Enemy> AllEnemies = [];
        private readonly ZoktaiAddresses _memAddresses;
        private readonly MemoryValues _memoryValues;

        public ZoktaiEntities(MemoryValues memoryValues, ZoktaiAddresses zoktaiAddresses) {
            _memoryValues = memoryValues;
            _memAddresses = zoktaiAddresses;
            InitPlayableCharacters();
            InitNpcs();
            InitCharacters();
            InitEnemies();
        }

        /// <summary>Init Character instances for playable characters</summary>
        private void InitPlayableCharacters() {
            PlayableCharacters.Add("Django", new Character("Django", 0));
            PlayableCharacters.Add("Black Django", new Character("Black Django", 1));
            PlayableCharacters.Add("Bat", new Character("Bat", 2));         // "Bat change" form
            PlayableCharacters.Add("Mouse", new Character("Mouse", 3));     // "Mouse change" form
            // PlayableCharacters.Add("", new Character("", 4));            // Unused
            PlayableCharacters.Add("Sabata", new Character("Sabata", 5));
        }

        /// <summary>Init Character instances for NPCs</summary>
        private void InitNpcs() {
            // Npcs.Add("Empty slot", new Character("Empty Slot", 65535));
        }

        ///<summary>Init the full list containing all characters</summary>
        private void InitCharacters() {
            // AllCharacters.Add("Empty slot", new Character("Empty Slot", 65535));
        }

        //<summary>Init the full list containing all enemies (mostly used for simulators)</summary>
        private void InitEnemies() {
            // AllEnemies.Add("Empty slot", new Enemy("Empty Slot", 65535));
        }
    }
}