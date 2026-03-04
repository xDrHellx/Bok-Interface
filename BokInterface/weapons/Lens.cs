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
        /// <summary>Resource library for retrieving the icon</summary>
        protected abstract string library { get; }

        public Lens(string name, uint value, string element, string icon = "") {
            this.name = name;
            this.value = value;
            this.element = element;
            SetIconResource(icon);
        }

        /// <summary>Simplified method for setting the instance's icon via resources</summary>
        /// <param name="icon">Icon string</param>
        /// <returns><c>Image</c>Resource</returns>
        protected void SetIconResource(string iconString) {
            icon = null;
            if (iconString != "" && library != "") {
                try {
                    icon = (Image)ResourceLoader.LoadResource(library, iconString);
                } catch { }
            }
        }
    }
}
