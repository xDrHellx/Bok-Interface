using System.Drawing;

namespace BokInterface.Weapons {
    /// <summary>Class for representing a Gun Lens in Shinbok</summary>
    class ShinbokLens : Lens {

        /// <summary>Indicate if obtained from event</summary>
        public bool eventLens;

        public ShinbokLens(string name, uint value, string element, string icon = "", bool eventLens = false) : base(name, value, element, icon) {
            this.eventLens = eventLens;

            if (icon != "") {
                try {
                    this.icon = (Image)ResourceLoader.LoadResource("ShinbokResources", icon);
                } catch { }
            }
        }
    }
}
