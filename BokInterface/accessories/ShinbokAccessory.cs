using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Shinbok</summary>
    class ShinbokAccessory : Accessory {

        /// <summary>Accessory level (determines results when used for solar forging)</summary>
        public int level = 1;
        /// <summary>The name of the set the accessory belongs to (Solar Django, Black Django, Rockman, ...)</summary>
        public string set = "";

        public ShinbokAccessory(string name, uint value, string type, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int defense = 0, int level = 0, string set = "") : base(name, value, type) {
            this.name = name;
            this.value = value;
            this.type = type;
            this.row = row;
            this.crossOver = crossOver;
            this.defense = defense;
            this.buyPrice = buyPrice;
            this.level = level;
            this.set = set;

            // Price for selling is always the buying price divided by 2 (or 0 if it cannot be sold)
            sellPrice = buyPrice > 0 ? buyPrice / 2 : 0;

            // If an icon was specified try getting & setting it to the property
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}