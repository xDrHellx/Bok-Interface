using System.Drawing;

namespace BokInterface.Accessories {
    ///<summary>Class for representing an accessory in Boktai DS / Lunar Knights</summary>
    class DsAccessory : Accessory {

        /// <summary>The name of the set the accessory belongs to (Solar Django, Black Django, Rockman, ...)</summary>
        // public string set = "";

        public DsAccessory(string name, uint value, string type, string icon = "", int buyPrice = 0, bool crossOver = false/*, string set = ""*/) : base(name, value, type, icon, 1, buyPrice, crossOver) {
            // this.set = set;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("DsResources", icon);
                } catch { }
            }
        }
    }
}
