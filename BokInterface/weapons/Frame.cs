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
        /// <summary>Resource library for retrieving the icon</summary>
        protected abstract string library { get; }

        public Frame(string name, string power, string icon = "") {
            this.name = name;
            this.power = power;
            SetIconResource(icon);
        }

        /// <summary>Simplified method for setting the instance's icon via resources</summary>
        /// <param name="icon">Icon string</param>
        /// <returns><c>Image</c>Resource</returns>
        protected void SetIconResource(string iconString) {
            icon = null;
            if (iconString != "") {
                try {
                    icon = library != "" ? (Image)ResourceLoader.LoadResource(library, iconString) : (Image)Properties.Resources.ResourceManager.GetObject(iconString);
                } catch { }
            }
        }
    }
}
