using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Base class for representing a Gun Frame</summary>
    abstract class Frame {

        /// <summary>Frame name</summary>
        public string name;
        /// <summary>Frame icon</summary>
        public Image? icon = null;
        /// <summary>Power stat (damage, S > A > B > C > D)</summary>
        public string power;

        public Frame(string name, string power, string icon = "") {
            this.name = name;
            this.power = power;

            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
