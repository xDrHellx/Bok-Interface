using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Base class for representing a Gun Lens</summary>
    abstract class Lens {

        /// <summary>Lens name</summary>
        public string name;
        /// <summary>Value (decimal)</summary>
        public uint value;
        /// <summary>Lens element</summary>
        public string element;
        /// <summary>Lens icon</summary>
        public Image? icon = null;

        public Lens(string name, uint value, string element, string icon = "") {
            this.name = name;
            this.value = value;
            this.element = element;

            if (icon != "") {
                try {
                    this.icon = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                } catch { }
            }
        }
    }
}
