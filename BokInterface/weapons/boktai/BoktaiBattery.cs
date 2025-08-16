using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Battery in Boktai</summary>
    class BoktaiBattery {

        /// <summary>Battery name</summary>
        public string name;
        /// <summary>Battery icon</summary>
        public Image? icon = null;

        public BoktaiBattery(string name, string icon = "") {
            this.name = name;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("BoktaiResources", icon);
                } catch { }
            }
        }
    }
}
