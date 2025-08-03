using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Shinbok</summary>
    class ShinbokAccessory : Accessory {

        /// <summary>Accessory level (determines results when used for solar forging)</summary>
        public int level = 1;
        /// <summary>The name of the set the accessory belongs to (Solar Django, Black Django, Rockman, ...)</summary>
        public string set = "";

        public ShinbokAccessory(string name, uint value, string type, string icon = "", int row = 1, int buyPrice = 0, bool crossOver = false, int level = 0, string set = "") : base(name, value, type, icon, row, buyPrice, crossOver) {
            this.level = level;
            this.set = set;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}
