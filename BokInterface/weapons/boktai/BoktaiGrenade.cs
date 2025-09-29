using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Grenade in Boktai</summary>
    class BoktaiGrenade {

        /// <summary>Grenade name</summary>
        public string name;
        /// <summary>Grenade icon</summary>
        public Image? icon = null;

        public BoktaiGrenade(string name, string icon = "") {
            this.name = name;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("BoktaiResources", icon);
                } catch { }
            }
        }
    }
}
