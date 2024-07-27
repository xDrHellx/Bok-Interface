using System.Collections.Generic;

using BokInterface.Weapons.Accessories;

namespace BokInterface.Accessories {
    /// <summary>Class for Zoktai accessories (protectors) instances and lists</summary>
    class ZoktaiAccessories {

        public Dictionary<string, Accessory> All = [];

        public ZoktaiAccessories() {
            InitProtectorsList();
        }

        ///<summary>Init accessory instances for protectors</summary>
        private void InitProtectorsList() {
            // All.Add("No protector", new Accessory("No protector", 0, ""));
        }
    }
}