using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Zoktai (also called protector)</summary>
    class ZoktaiAccessory : Accessory {

        ///<summary>Accessory defense (refered as Durability in-game)</summary>
        public int defense;
        /// <summary>Accessory weight</summary>
        public int weight;

        public ZoktaiAccessory(string name, uint value, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int defense = 0, int weight = 0) : base(name, value, "body", icon, row, buyPrice, crossOver) {
            this.defense = defense;
            this.weight = weight;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}