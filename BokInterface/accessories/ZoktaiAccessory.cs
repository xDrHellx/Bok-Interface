using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Zoktai (also called protector)</summary>
    class ZoktaiAccessory : Accessory {

        public ZoktaiAccessory(string name, uint value, string type, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int defense = 0, int weight = 0) : base(name, value, type) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.row = row;
            this.crossOver = crossOver;
            this.defense = defense;
            this.weight = weight;
            this.buyPrice = buyPrice;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ZoktaiResources", icon);
                } catch { }
            }
        }
    }
}