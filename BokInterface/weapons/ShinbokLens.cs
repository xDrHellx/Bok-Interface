using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Lens in Shinbok</summary>
    class ShinbokLens {

        /// <summary>Lens name</summary>
        public string name;
        /// <summary>Value (decimal)</summary>
        public uint value;
        /// <summary>Lens element</summary>
        public string element;
        /// <summary>Lens icon</summary>
        public Image? icon = null;
        /// <summary>Indicates if obtained from event</summary>
        public bool eventLens;

        public ShinbokLens(string name, uint value, string element, string icon = "", bool eventLens = false) {
            this.name = name;
            this.value = value;
            this.element = element;
            this.eventLens = eventLens;
            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}