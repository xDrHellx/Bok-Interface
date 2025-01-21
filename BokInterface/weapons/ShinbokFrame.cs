using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Frame in Shinbok</summary>
    class ShinbokFrame {

        /// <summary>Frame name</summary>
        public string name;
        /// <summary>Value (decimal)</summary>
        public uint value;
        /// <summary>Frame icon</summary>
        public Image? icon = null;
        /// <summary>Power stat (damage, S > A > B > C > D)</summary>
        public string power;
        /// <summary>Energy cost stat (S > A > B > C > D)</summary>
        public string cost;

        public ShinbokFrame(string name, uint value, string power, string cost, string icon = "") {
            this.name = name;
            this.value = value;
            this.power = power;
            this.cost = cost;
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}